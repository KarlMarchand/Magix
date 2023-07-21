namespace magix_api.Dtos.PlayerDto
{
    public class GameServerPlayerDto
    {
        public string? key { get; set; }
        public string? className { get; set; }
        public string? userType { get; set; }
        public int winCount { get; set; }
        public int lossCount { get; set; }
        public int trophies { get; set; }
        public int bestTrophyScore { get; set; }
        public string lastLogin { get; set; } = string.Empty;
        public string? welcomeText { get; set; }
    }
}