using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.GameDto
{
    public class GameStateFromServerDto
    {
        public string? Username { get; set; }
        public int? RemainingTurnTime { get; set; }
        public bool? YourTurn { get; set; }
        public bool? HeroPowerAlreadyUsed { get; set; }
        public int? Hp { get; set; }
        public int? Mp { get; set; }
        public int? MaxMp { get; set; }
        public List<CardFromGameServerDto> Hand { get; set; } = new();
        public List<CardFromGameServerDto> Board { get; set; } = new();
        public string? WelcomeText { get; set; }
        public string? HeroClass { get; set; }
        public int? RemainingCardsCount { get; set; }
        public GameStateFromServerDto? Opponent { get; set; }
        public List<LatestActionDto> LatestActions { get; set; } = new();
        public int? HandSize { get; set; }
    }

    public class LatestActionDto
    {
        public int? Id { get; set; }
        public string? From { get; set; }
        public ActionDetailsDto? Action { get; set; }
    }

    public class ActionDetailsDto
    {
        public string? Type { get; set; }
        public int? Uid { get; set; }
        public int? Id { get; set; }
    }
}