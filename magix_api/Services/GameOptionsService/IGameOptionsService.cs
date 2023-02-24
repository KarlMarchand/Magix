using magix_api.Dtos.CardDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.FactionDto;
using magix_api.Dtos.DeckDto;

namespace magix_api.Services.GameOptionsService
{
    public interface IGameOptionsService
    {
        Task<ServiceResponse<GetAvailableOptionsDto>> GetAllOptions();
        Task<ServiceResponse<List<GetCardDto>>> GetAllCards();
        Task<ServiceResponse<List<GetFactionDto>>> GetAllFactions();
        Task<ServiceResponse<List<GetHeroDto>>> GetAllHeroes();
        Task<ServiceResponse<List<GetTalentDto>>> GetAllTalents();
    }
}