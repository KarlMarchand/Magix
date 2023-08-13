using AutoMapper;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.TalentDto;
using magix_api.Repositories;
using magix_api.utils;
using System.Text.Json;

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

        public async Task<ServiceResponse<GetDeckDto>> CreateDeck(string playerKey, int playerId, DeckDto newDeck)
        {
            // TODO: Add possibility to create a draft
            // TODO: Call GetAllOptions on database creation and then Add seeding of decks, players and stats
            return await UpsertDeck(playerKey, playerId, newDeck, _deckRepo.CreateDeck);
        }

        public async Task<ServiceResponse<GetDeckDto>> UpdateDeck(string playerKey, int playerId, DeckDto updateDeckDto)
        {
            return await UpsertDeck(playerKey, playerId, updateDeckDto, _deckRepo.UpdateDeck);
        }

        // Insert or Update a deck
        private async Task<ServiceResponse<GetDeckDto>> UpsertDeck(string playerKey, int playerId, DeckDto deckDto, Func<Deck, Task<Deck>> saveAction)
        {
            ServiceResponse<GetDeckDto> response = new();

            var deck = _mapper.Map<Deck>(deckDto);

            deck.PlayerId = playerId;

            // Validate if the deck is conform to the business rules
            var validationResponse = await ValidateDeck(deck);
            if (!validationResponse.Success)
            {
                response.Success = false;
                response.Message = validationResponse.Message;
                return response;
            }

            if (await ActivateDeck(playerKey, deck))
            {
                // Convert the list of card Ids into a list of Cards
                List<int> cardIds = deckDto.Cards.Select(c => c.Id).ToList();
                deck.Cards = await _cardRepo.GetCardsByIds(cardIds);

                // Transform the repeating cards in a Id -> Qty
                var cardCounts = cardIds.GroupBy(id => id).ToDictionary(group => group.Key, group => group.Count());
                deck.DeckCards = cardCounts.Select(kv => new DeckCard { CardId = kv.Key, Quantity = kv.Value }).ToList();

                response.Data = _mapper.Map<GetDeckDto>(await saveAction(deck));
                response.Success = response.Data != null;
            }
            else
            {
                response.Success = false;
                response.Message = "InvalidDeckGameServer";
            }
            return response;
        }

        public async Task<ServiceResponse<GetDeckDto>> GetDeck(Guid id)
        {
            ServiceResponse<GetDeckDto> response = new();
            var deck = await _deckRepo.GetDeck(id);
            if (deck != null)
            {
                response.Data = _mapper.Map<GetDeckDto>(deck);
            }
            else
            {
                response.Success = false;
                response.Message = "DeckNotFound";
            }
            return response;
        }

        public async Task<ServiceResponse<GetDeckDto>> GetActiveDeck(int playerId)
        {
            ServiceResponse<GetDeckDto> response = new();
            var deck = await _deckRepo.GetDeck();
            if (deck != null)
            {
                response.Data = _mapper.Map<GetDeckDto>(deck);
            }
            else
            {
                response.Success = false;
                response.Message = "DeckNotFound";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetDeckDto>>> GetAllDecks(int playerId)
        {
            ServiceResponse<List<GetDeckDto>> response = new();
            response.Data = _mapper.Map<List<GetDeckDto>>(await _deckRepo.GetAllDecks(playerId));
            return response;
        }

        // Will delete the deck only if there's another deck to switch to after since the Game Server needs an active deck at all time
        public async Task<ServiceResponse<GetDeckDto>> DeleteDeck(string playerKey, int playerId, Guid deckId)
        {
            ServiceResponse<GetDeckDto> response = new();

            // To prevent a desynchronization with the gameServer, we switch to another deck after
            var nextDeck = (await _deckRepo.GetAllDecks(playerId)).Where(d => d.Id != deckId).FirstOrDefault();
            if (nextDeck != null)
            {
                var couldDelete = await _deckRepo.DeleteDeck(deckId, playerId);
                if (couldDelete && await ActivateDeck(playerKey, nextDeck))
                {
                    response.Success = true;
                    response.Data = _mapper.Map<GetDeckDto>(nextDeck);
                }
                else
                {
                    response.Success = false;
                    response.Message = couldDelete ? "InvalidDeckGameServer" : "CouldNotDelete";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "NoOtherDeck";
            }
            return response;
        }

        // Switch a deck for another one
        public async Task<ServiceResponse<GetDeckDto>> SwitchDeck(string playerKey, int playerId, Guid id)
        {
            ServiceResponse<GetDeckDto> response = new();

            var deck = await _deckRepo.GetDeck(id);

            if (deck != null)
            {
                if (await ActivateDeck(playerKey, deck))
                {
                    response.Data = _mapper.Map<GetDeckDto>(deck);
                    response.Success = response.Data != null;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "DeckNotFound";
            }
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

        // Activate the passed deck in the database and Game Server
        private async Task<bool> ActivateDeck(string playerKey, Deck deck)
        {
            bool deckSavedToServer = await SaveDeckToGameServer(playerKey, deck);

            if (deckSavedToServer)
            {
                await _deckRepo.UpdateActiveDeck(deck);
            }
            return deckSavedToServer;
        }

        // Send the deck to the Game Server and return if the deck was accepted and saved
        private async Task<bool> SaveDeckToGameServer(string playerKey, Deck deck)
        {
            List<DeckCardDto> cardList = deck.Cards!.Select(c => new DeckCardDto() { Id = c.Id }).ToList();
            string heroName = deck.Hero!.GetServerName();
            string talentName = deck.Talent!.GetServerName();

            string apiUrl = "/users/deck/save";

            string jsonDeck = JsonSerializer.Serialize(cardList);

            Dictionary<string, string> data = new()
            {
                {"key", playerKey},
                {"deck", jsonDeck},
                {"className", heroName},
                {"initialTalent", talentName}
            };

            ServerResponse<bool> res = await GameServerAPI.CallApi<bool>(apiUrl, data);

            return res.IsValid && res.Content;
        }

        // Validate the business rules for a new or updated deck
        private async Task<ServiceResponse<bool>> ValidateDeck(Deck deck)
        {
            var response = new ServiceResponse<bool>();

            if (deck.Cards == null || deck.Cards.Count != 30)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "InvalidNumberOfCards";
                return response;
            }

            deck.Hero = deck.Hero ?? await _heroRepo.GetHeroById(deck.HeroId);
            if (deck.Hero == null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "CantFindHero";
                return response;
            }

            deck.Talent = deck.Talent ?? await _talentRepo.GetTalentById(deck.TalentId);
            if (deck.Talent == null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "CantFindTalent";
                return response;
            }

            deck.Faction = deck.Faction ?? await _factionRepo.GetFactionById(deck.FactionId);
            if (deck.Faction == null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "CantFindFaction";
                return response;
            }

            response.Data = true;
            response.Success = true;
            return response;
        }

    }
}