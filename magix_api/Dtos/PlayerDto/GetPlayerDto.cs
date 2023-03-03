
namespace magix_api.Dtos.PlayerDto
{
    public class GetPlayerDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = default!;

        public string? ClassName { get; set; }

        public int WinCount { get; set; }

        public int LossCount { get; set; }

        public DateTime LastLogin { get; set; }

        public string? WelcomeText { get; set; }

        public int Trophies { get; set; }

        public int BestTrophyScore { get; set; }

        public virtual List<Deck>? Decks { get; set; }

        public virtual List<Game>? Games { get; set; }

        public virtual List<PlayedCard>? PlayedCards { get; set; }

        public string Key { get; set; } = default!;

        public PlayerStat? Stats { get; set; }
    }
}