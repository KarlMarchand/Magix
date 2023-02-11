
namespace magix_api.Dtos.CardDto
{
    public class GetCardDto
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public List<string> Mechanics { get; set; } = null!;
        public string Dedicated { get; set; } = null!;
        public string CardName { get; set; } = null!;
        public string Faction { get; set; } = null!;
        public string Sound { get; set; } = null!;
    }
}