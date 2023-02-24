namespace magix_api;

public partial class PlayerStat
{
    public long GamePlayed { get; set; }

    public decimal? Wins { get; set; }

    public decimal? Loses { get; set; }

    public decimal? RatioWins { get; set; }

    public string? TopCards { get; set; }
}
