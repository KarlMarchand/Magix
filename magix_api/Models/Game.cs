using System;
using System.Collections.Generic;

namespace magix_api;

public partial class Game
{
    public int Id { get; set; }

    public int Player { get; set; }

    public string? Class { get; set; }

    public string Opponent { get; set; } = null!;

    public DateOnly Date { get; set; }

    public bool Won { get; set; }

    public virtual Player PlayerNavigation { get; set; } = null!;
}
