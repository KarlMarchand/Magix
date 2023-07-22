using System.Text.Json;
using AutoMapper;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Repositories;
using magix_api.utils;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {
        private readonly ITalentRepo _talentRepo;
        private readonly IHeroRepo _heroRepo;
        private readonly ICardRepo _cardRepo;
        private readonly IDeckRepository _deckRepo;
        private readonly IMapper _mapper;
        private readonly IFactionsRepo _factionRepo;

        public DeckService(IMapper mapper, IDeckRepository deckRepo, ITalentRepo talentRepo, IHeroRepo heroRepo, ICardRepo cardRepo, IFactionsRepo factionRepo)
        {
            _mapper = mapper;
            _deckRepo = deckRepo;
            _cardRepo = cardRepo;
            _heroRepo = heroRepo;
            _talentRepo = talentRepo;
            _factionRepo = factionRepo;
        }

        public async Task<ServiceResponse<GetDeckDto>> CreateDeck(string playerKey, int playerId, DeckDto deck)
        {
            // TODO: Ajouter la possibilité de faire des brouillons

            ServiceResponse<GetDeckDto> response = new(){Success = false};

            var hero = await _heroRepo.GetHeroById(deck.HeroId);
            var talent = await _talentRepo.GetTalentById(deck.TalentId);
            var faction = await _factionRepo.GetFactionById(deck.FactionId);

            if (hero != null && talent != null)
            {
                if (await SendDeckServer(playerKey, deck.Cards, hero.GetServerName(), talent.GetServerName()))
                {
                    List<int> cardIds = deck.Cards.Select(c => c.Id).ToList();
                    List<Card> cards = await _cardRepo.GetCardsByIds(cardIds);

                    var cardCounts = cardIds.GroupBy(id => id).ToDictionary(group => group.Key, group => group.Count());

                    var newDeck = new Deck
                    {
                        Name = deck.Name,
                        PlayerId = playerId,
                        Talent = talent,
                        Hero = hero,
                        Faction = faction,
                        Active = deck.Active,
                        Cards = cards,
                        DeckCards = cardCounts.Select(kv => new DeckCard { CardId = kv.Key, Quantity = kv.Value }).ToList()
                    };
                    Deck createdDeck = await _deckRepo.CreateDeck(newDeck);
                    response.Data = _mapper.Map<GetDeckDto>(createdDeck);
                    response.Success = response.Data != null;
                }
            } 
            else
            {
                response.Message = "Couldn't find the hero or talent";
            }

            return response;
        }

        public async Task<ServiceResponse<GetDeckDto>> DeleteDeck(string playerKey, int playerId, int deckId)
        {
            ServiceResponse<GetDeckDto> response = new();
            response.Success = await _deckRepo.DeleteDeck(deckId);
            if (response.Success)
            {
                // To prevent a desynchronization with the gameServer, we switch to another deck after
                var newDeck = (await _deckRepo.GetAllDecks(playerId)).FirstOrDefault();
                if (newDeck != null && await SendDeckServer(playerKey, newDeck))
                {
                    response.Data = _mapper.Map<GetDeckDto>(newDeck);
                }
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetDeckDto>>> GetAllDecks(int playerId)
        {
            ServiceResponse<List<GetDeckDto>> response = new();
            response.Data = _mapper.Map<List<GetDeckDto>>(await _deckRepo.GetAllDecks(playerId));
            return response;
        }

        public async Task<ServiceResponse<GetDeckDto>> UpdateDeck(string playerKey, Deck deck)
        {
            ServiceResponse<GetDeckDto> response = new();

            var successfullySent = await SendDeckServer(playerKey, deck);

            if (successfullySent)
            {
                response.Data = _mapper.Map<GetDeckDto>(await _deckRepo.UpdateDeck(deck));
                response.Success = response.Data != null;
            }

            return response;
        }

        public async Task<ServiceResponse<GetDeckDto>> SwitchDeck(string playerKey, int id)
        {
            ServiceResponse<GetDeckDto> response = new();

            var deck = await _deckRepo.GetDeck(id);

            var successfullySent = await SendDeckServer(playerKey, deck);
            if (successfullySent)
            {
                response.Data = _mapper.Map<GetDeckDto>(deck);
                response.Success = response.Data != null;
            }

            return response;
        }

        public async Task<ServiceResponse<GetDeckDto>> GetDeck(int id)
        {
            ServiceResponse<GetDeckDto> response = new();
            response.Data = _mapper.Map<GetDeckDto>(await _deckRepo.GetDeck(id));
            return response;
        }

        public async Task<ServiceResponse<GetAvailableOptionsDto>> GetAllOptions()
        {
            var serviceResponse = new ServiceResponse<GetAvailableOptionsDto>();
            var factions = await _factionRepo.GetAllFactions();
            var talents = await _talentRepo.GetAllTalents();
            var heroes = await _heroRepo.GetAllHeroes();
            var cards = await _cardRepo.GetAllCards();
            serviceResponse.Data = new GetAvailableOptionsDto
            {
                Cards = _mapper.Map<List<GetCardDto>>(cards),
                Talents = _mapper.Map<List<GetTalentDto>>(talents),
                Heroes = _mapper.Map<List<GetHeroDto>>(heroes),
                Factions = _mapper.Map<List<GetFactionDto>>(factions),
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCardDto>>> GetAllCards()
        {
            var serviceResponse = new ServiceResponse<List<GetCardDto>>();
            List<Card> cardList = await _cardRepo.GetAllCards();
            serviceResponse.Data = _mapper.Map<List<Card>, List<GetCardDto>>(cardList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFactionDto>>> GetAllFactions()
        {
            var serviceResponse = new ServiceResponse<List<GetFactionDto>>();
            List<Faction> factionList = await _factionRepo.GetAllFactions();
            serviceResponse.Data = _mapper.Map<List<Faction>, List<GetFactionDto>>(factionList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes()
        {
            var serviceResponse = new ServiceResponse<List<GetHeroDto>>();
            List<Hero> heroList = await _heroRepo.GetAllHeroes();
            serviceResponse.Data = _mapper.Map<List<Hero>, List<GetHeroDto>>(heroList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTalentDto>>> GetAllTalents()
        {
            var serviceResponse = new ServiceResponse<List<GetTalentDto>>();
            List<Talent> talentList = await _talentRepo.GetAllTalents();
            serviceResponse.Data = _mapper.Map<List<Talent>, List<GetTalentDto>>(talentList);
            return serviceResponse;
        }

        private static async Task<bool> SendDeckServer(string playerKey, Deck deck)
        {
            if (deck.Hero == null || deck.Talent == null || deck.Cards == null || deck.Cards.Count < 30)
            {
                return false;
            }
            List<DeckCardDto> cardList = deck.Cards.Select(c => new DeckCardDto(){Id = c.Id}).ToList();
            string className = deck.Hero.GetServerName();
            string initialTalent = deck.Talent.GetServerName();
            return await SendDeckServer(playerKey, cardList, className, initialTalent);
        }

        private static async Task<bool> SendDeckServer(string playerKey, List<DeckCardDto> deck, string className, string initialTalent)
        {
            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(initialTalent) || deck.Count < 30)
            {
                return false;
            }
            //string apiUrl = "/users/deck/save";

            //string jsonDeck = JsonSerializer.Serialize(deck);

            //Dictionary<string, string> data = new()
            //{
            //    {"key", playerKey},
            //    {"deck", jsonDeck},
            //    {"className", className},
            //    {"initialTalent", initialTalent}
            //};

            //ServerResponse<bool> res = await GameServerAPI.CallApi<bool>(apiUrl, data);

            //return res.IsValid && res.Content;
            return true;
        }
    }
}