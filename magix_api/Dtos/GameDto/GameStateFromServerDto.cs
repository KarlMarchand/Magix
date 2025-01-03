using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.GameDto
{
    public class GameStateFromServerDto : BaseGameStateFromServerDto
    {
        public int RemainingTurnTime { get; set; }
        public bool YourTurn { get; set; }
        public bool HeroPowerAlreadyUsed { get; set; }
        public int MaxMp { get; set; }
        public List<CardFromGameServerDto> Hand { get; set; } = new();
        public OpponentGameStateFromServerDto Opponent { get; set; } = new();
        public List<LatestActionFromServerDto> LatestActions { get; set; } = new();
    }

    public class BaseGameStateFromServerDto
    {
        public string Username { get; set; } = string.Empty;
        public string HeroClass { get; set; } = string.Empty;
        public int Hp { get; set; }
        public int Mp { get; set; }
        public List<CardFromGameServerDto> Board { get; set; } = new();
        public string WelcomeText { get; set; } = string.Empty;
        public int RemainingCardsCount { get; set; }
    }

    public class OpponentGameStateFromServerDto : BaseGameStateFromServerDto
    {
        public int HandSize { get; set; }
    }

    public class LatestActionFromServerDto
    {
        public int Id { get; set; }
        public string From { get; set; } = string.Empty;
        public ActionDetailsFromServerDto? Action { get; set; }
    }

    public class ActionDetailsFromServerDto
    {
        public string Type { get; set; } = string.Empty;
        public int Uid { get; set; }
        public int Id { get; set; }
    }
}