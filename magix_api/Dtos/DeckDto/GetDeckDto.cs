namespace magix_api.Dtos.DeckDto
{
    public class GetDeckDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public Hero Hero { get; set; }
        public Talent Talent { get; set; }
        public Faction Faction { get; set; }
        public bool Active { get; set; } = true;
        public List<Game>? Games { get; }
        public List<Card> Cards { get; set; }
    }
}
