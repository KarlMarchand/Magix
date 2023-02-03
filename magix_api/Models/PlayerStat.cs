using System;
using System.Collections.Generic;

namespace magix_api;

public partial class PlayerStat
{
    public int? Id { get; set; }

    public string? Username { get; set; }

    public string? Class { get; set; }

    public int? Trophies { get; set; }

    public int? BestTrophyScore { get; set; }

    public string? Faction { get; set; }

    public long? GamePlayed { get; set; }

    public decimal? Wins { get; set; }

    public decimal? Loses { get; set; }

    public decimal? RatioWins { get; set; }

    public string? TopCards { get; set; }
}
