namespace magix_api.Dtos.GameDto
{
    public class SaveGameDto
    {
        public string Opponent { get; set; }
        public bool Victory { get; set; }
        public Guid DeckId { get; set; }
    }
}
