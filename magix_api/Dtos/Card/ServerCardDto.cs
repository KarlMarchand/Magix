
namespace magix_api.Dtos.Card
{
    public class ServerCardDto
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public List<string> Mechanics { get; set; }
        public string Dedicated { get; set; }
    }
}