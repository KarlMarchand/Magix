using magix_api.Dtos.CardDto;
using magix_api.Dtos.TalentDto;
using magix_api.Dtos.HeroDto;
using magix_api.Dtos.FactionDto;

namespace magix_api.Dtos.DeckDto
{
    public class GetAvailableOptionsDto
    {
        public List<GetCardDto> Cards { get; set; } = default!;
        public List<GetTalentDto> Talents { get; set; } = default!;
        public List<GetHeroDto> Heroes { get; set; } = default!;
        public List<GetFactionDto> Factions { get; set; } = default!;
    }
}