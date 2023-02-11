using AutoMapper;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.FactionDto;
using magix_api.Repositories;

namespace magix_api.Services.DeckService
{
    public class DeckService : IDeckService
    {
        private readonly IMapper _mapper;
        private readonly IDeckRepository _deckRepository;

        public DeckService(
                IMapper mapper,
                IDeckRepository deckRepository
            )
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetAvailableOptionsDto>> GetAllOptions()
        {
            var serviceResponse = new ServiceResponse<GetAvailableOptionsDto>();
            AvailableOptions options = new();
            options.Cards = await _deckRepository.GetAllCards();
            options.Talents = await _deckRepository.GetAllTalents();
            options.Heroes = await _deckRepository.GetAllHeroes();
            options.Factions = await _deckRepository.GetAllFactions();
            serviceResponse.Data = _mapper.Map<GetAvailableOptionsDto>(options);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCardDto>>> GetAllCards()
        {
            var serviceResponse = new ServiceResponse<List<GetCardDto>>();
            List<Card> cardList = await _deckRepository.GetAllCards();
            serviceResponse.Data = _mapper.Map<List<Card>, List<GetCardDto>>(cardList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFactionDto>>> GetAllFactions()
        {
            var serviceResponse = new ServiceResponse<List<GetFactionDto>>();
            List<Faction> factionList = await _deckRepository.GetAllFactions();
            serviceResponse.Data = _mapper.Map<List<Faction>, List<GetFactionDto>>(factionList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes()
        {
            var serviceResponse = new ServiceResponse<List<GetHeroDto>>();
            List<Hero> heroList = await _deckRepository.GetAllHeroes();
            serviceResponse.Data = _mapper.Map<List<Hero>, List<GetHeroDto>>(heroList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTalentDto>>> GetAllTalents()
        {
            var serviceResponse = new ServiceResponse<List<GetTalentDto>>();
            List<Talent> talentList = await _deckRepository.GetAllTalents();
            serviceResponse.Data = _mapper.Map<List<Talent>, List<GetTalentDto>>(talentList);
            return serviceResponse;
        }
    }
}