using AutoMapper;
using magix_api.Dtos.DeckDto;
using magix_api.Dtos.CardDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.FactionDto;
using magix_api.Repositories;

namespace magix_api.Services.GameOptionsService
{
    public class GameOptionsService
    {
        private readonly IMapper _mapper;
        private readonly IGameOptionsRepo _gameOptionsRepository;

        public GameOptionsService(
                IMapper mapper,
                IGameOptionsRepo gameOptionsRepository
            )
        {
            _gameOptionsRepository = gameOptionsRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetAvailableOptionsDto>> GetAllOptions()
        {
            var serviceResponse = new ServiceResponse<GetAvailableOptionsDto>();
            var cards = await _gameOptionsRepository.GetAllCards();
            var talents = await _gameOptionsRepository.GetAllTalents();
            var heroes = await _gameOptionsRepository.GetAllHeroes();
            var factions = await _gameOptionsRepository.GetAllFactions();
            serviceResponse.Data = new GetAvailableOptionsDto{
                Cards   = _mapper.Map<List<GetCardDto>>(cards),
                Talents = _mapper.Map<List<GetTalentDto>>(talents),
                Heroes  = _mapper.Map<List<GetHeroDto>>(heroes),
                Factions= _mapper.Map<List<GetFactionDto>>(factions),
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCardDto>>> GetAllCards()
        {
            var serviceResponse = new ServiceResponse<List<GetCardDto>>();
            List<Card> cardList = await _gameOptionsRepository.GetAllCards();
            serviceResponse.Data = _mapper.Map<List<Card>, List<GetCardDto>>(cardList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFactionDto>>> GetAllFactions()
        {
            var serviceResponse = new ServiceResponse<List<GetFactionDto>>();
            List<Faction> factionList = await _gameOptionsRepository.GetAllFactions();
            serviceResponse.Data = _mapper.Map<List<Faction>, List<GetFactionDto>>(factionList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes()
        {
            var serviceResponse = new ServiceResponse<List<GetHeroDto>>();
            List<Hero> heroList = await _gameOptionsRepository.GetAllHeroes();
            serviceResponse.Data = _mapper.Map<List<Hero>, List<GetHeroDto>>(heroList);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTalentDto>>> GetAllTalents()
        {
            var serviceResponse = new ServiceResponse<List<GetTalentDto>>();
            List<Talent> talentList = await _gameOptionsRepository.GetAllTalents();
            serviceResponse.Data = _mapper.Map<List<Talent>, List<GetTalentDto>>(talentList);
            return serviceResponse;
        }
    }
}