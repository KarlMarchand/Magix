namespace magix_api.Dtos.DeckDto
{
    public class SaveDeckDto
    {
        public Player Player { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Hero Hero { get; set; } = default!;

        public Talent Talent { get; set; } = default!;

        public Faction Faction { get; set; } = default!;

        public bool Active { get; set; } = true;

    }
}