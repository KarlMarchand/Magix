using magix_api.Dtos.CardDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.FactionDto;

namespace magix_api.Dtos.DeckDto
{
    public class GetAvailableOptionsDto
    {
        public List<GetCardDto> cards { get; set; } = default!;
        public List<GetTalentDto> talents { get; set; } = default!;
        public List<GetHeroDto> heroes { get; set; } = default!;
        public List<GetFactionDto> factions { get; set; } = default!;
    }
}