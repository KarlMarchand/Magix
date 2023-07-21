namespace magix_api.Dtos.PlayerDto
{
    public class GetPlayerStatsDto
    {
        public long GamePlayed { get; set; }
        public decimal? Wins { get; set; }
        public decimal? Loses { get; set; }
        public decimal? RatioWins { get; set; }
        public List<Card> TopCards { get; set; } = new List<Card>();
    }
}
