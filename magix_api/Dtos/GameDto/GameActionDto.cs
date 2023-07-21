namespace magix_api.Dtos.GameDto
{
    public class GameActionDto
    {
        public required string actionType { get; set; }
        public int? cardUid { get; set; }
        public int? targetUid { get; set; }
    }
}