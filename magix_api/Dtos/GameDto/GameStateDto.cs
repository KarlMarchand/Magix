using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.GameDto
{
    // TODO: Must do transition from server card to my card

    public class GameStateDto
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
}