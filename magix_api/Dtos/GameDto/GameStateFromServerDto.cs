namespace magix_api.Dtos.GameDto
{
    public class GameStateFromServerDto
    {
        public string? username;
        public int? RemainingTurnTime;
        public bool? YourTurn;
        public bool? HeroPowerAlreadyUsed;
        public int? Hp;
        public int? Mp;
        public int? MaxMp;
        public List<Card> Hand = new();
        public List<Card> Board = new();
        public string? WelcomeText;
        public string? HeroClass;
        public int? RemainingCardsCount;
        public GameStateFromServerDto? Opponent;
        public List<string>? LatestActions;
        public int? handSize;
    }
}