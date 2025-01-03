using magix_api.Dtos.HeroDto;

namespace magix_api.Dtos.GameDto;

public class GameStateDto : BaseGameStateDto
{
    public int RemainingTurnTime { get; set; }
    public bool YourTurn { get; set; }
    public bool HeroPowerAlreadyUsed { get; set; }
    public int MaxMp { get; set; }
    public List<Card> Hand { get; set; } = new();
    public OpponentGameStateDto Opponent { get; set; } = new();
    public List<LatestActionDto> LatestActions { get; set; } = new();
}

public class BaseGameStateDto
{
    public string Username { get; set; } = string.Empty;
    public GetHeroDto? Hero { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public List<Card> Board { get; set; } = new();
    public string WelcomeText { get; set; } = string.Empty;
    public int RemainingCardsCount { get; set; }
}

public class OpponentGameStateDto : BaseGameStateDto
{
    public int HandSize { get; set; }
}

public class LatestActionDto
{
    public int Id { get; set; }
    public string From { get; set; } = string.Empty;
    public ActionDetailsDto? Action { get; set; }
}

public class ActionDetailsDto
{
    public string Type { get; set; } = string.Empty;
    public int Uid { get; set; }
    public int Id { get; set; }
}
