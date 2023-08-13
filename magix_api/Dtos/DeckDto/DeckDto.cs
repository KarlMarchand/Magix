using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.DeckDto
{
    public class DeckDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = default!;
        public int HeroId { get; set; }
        public int TalentId { get; set; }
        public int FactionId { get; set; }
        public List<DeckCardDto> Cards { get; set; } = new();
    }
}