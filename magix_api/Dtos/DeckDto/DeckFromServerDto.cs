using magix_api.Dtos.CardDto;

namespace magix_api.Dtos.DeckDto
{
    public class DeckFromServerDto
    {
        public string ClassName { get; set; } = string.Empty;
        public string InitialTalent { get; set; } = string.Empty;
        public List<CardFromGameServerDto> Deck { get; set; } = new List<CardFromGameServerDto>();
    }
}
