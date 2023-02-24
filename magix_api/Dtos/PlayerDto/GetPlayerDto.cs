
namespace magix_api.Dtos.PlayerDto
{
    public class GetPlayerDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = default!;
        public string Username { get; set; } = "";
        public int Trophies { get; set; } = 0;
        public int BestTrophyScore { get; set; } = 0;
        public long GamePlayed { get; set; } = 0;
        public decimal Wins { get; set; } = 0;
        public decimal Loses { get; set; } = 0;
        public decimal? RatioWins { get; set; }
        public string TopCards { get; set; } = string.Empty;
    }
}