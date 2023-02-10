
namespace magix_api.Dtos.CardDto
{
    public class ServerCardDto
    {
        public int id { get; set; }
        public int cost { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public List<string> mechanics { get; set; }
        public string dedicated { get; set; }
    }
}