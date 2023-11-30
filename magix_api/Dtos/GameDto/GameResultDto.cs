namespace magix_api.Dtos.GameDto
{
    public class GameResultDto
    {
        public Guid Id { get; set; }
        public Deck? Deck { get; set; }
        public string? Opponent { get; set; }
        public DateTime Date { get; set; }
        public bool Won { get; set; }
    }
}
