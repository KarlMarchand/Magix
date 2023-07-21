using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.DeckDto
{
    public class DeckDto
    {
        public string Name { get; set; } = default!;
        public int HeroId { get; set; }
        public int TalentId { get; set; }
        public int FactionId { get; set; }
        public bool Active { get; set; } = true;
        public List<DeckCardDto> Cards { get; set; } = new();
    }
}