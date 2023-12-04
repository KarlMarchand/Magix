namespace magix_api.Dtos.HeroDto
{
    public class GetHeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Power { get; set; } = default!;
    }
}