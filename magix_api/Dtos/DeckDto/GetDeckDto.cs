using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.DeckDto
{
    public class GetDeckDto
    {
        public Guid Id { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; } = String.Empty;
        public Hero? Hero { get; set; }
        public int HeroId { get; set; }
        public Talent? Talent { get; set; }
        public int TalentId { get; set; }
        public Faction? Faction { get; set; }
        public int FactionId { get; set; }
        public bool Active { get; set; }
        public List<GetCardDto>? Cards { get; set; }
    }
}
