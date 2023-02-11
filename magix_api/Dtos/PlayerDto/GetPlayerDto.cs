
namespace magix_api.Dtos.PlayerDto
{
    public class GetPlayerDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Username { get; set; } = "";
        public int Trophies { get; set; } = 0;
        public int BestTrophyScore { get; set; } = 0;
    }
}