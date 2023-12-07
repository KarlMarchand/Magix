namespace magix_api.Dtos.PlayerDto
{
    public class GetPlayerDto
    {
        public string Username { get; set; } = default!;
        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public DateTime LastLogin { get; set; }
        public string? WelcomeText { get; set; }
        public int Trophies { get; set; }
        public int BestTrophyScore { get; set; }
        public string? Token { get; set; }
    }
}