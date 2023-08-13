namespace magix_api.Dtos.CardDto
{
    public class CardFromGameServerDto
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public List<string>? Mechanics { get; set; }
        public string? Dedicated { get; set; }
        public int? Uid { get; set; }
        public int? BaseHP { get; set; }
        public string? State { get; set; }
    }
}
