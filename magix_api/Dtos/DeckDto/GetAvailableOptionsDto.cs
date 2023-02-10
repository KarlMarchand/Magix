using magix_api.Dtos.CardDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.FactionDto;

namespace magix_api.Dtos.DeckDto
{
    public class GetAvailableOptionsDto
    {
        public List<GetCardDto> cards { get; set; }
        public List<GetTalentDto> talents { get; set; }
        public List<GetHeroDto> heroes { get; set; }
        public List<GetFactionDto> factions { get; set; }
    }
}