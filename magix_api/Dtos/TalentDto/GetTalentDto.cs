namespace magix_api.Dtos.TalentDto
{
    public class GetTalentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}