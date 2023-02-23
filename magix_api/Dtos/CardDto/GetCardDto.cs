
namespace magix_api.Dtos.CardDto
{
    public class GetCardDto
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public List<string> Mechanics { get; set; } = default!;
        public string Dedicated { get; set; } = default!;
        public string CardName { get; set; } = default!;
        public string Faction { get; set; } = default!;
        public string Sound { get; set; } = default!;
    }
}