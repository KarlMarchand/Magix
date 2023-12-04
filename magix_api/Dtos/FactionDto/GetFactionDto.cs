namespace magix_api.Dtos.FactionDto
{
    public class GetFactionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}