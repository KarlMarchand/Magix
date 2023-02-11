
namespace magix_api.Dtos.PlayerDto
{
    public class ServerPlayerDto
    {
        public string Username { get; set; } = "";

        public string? Class { get; set; }

        public int Trophies { get; set; } = 0;

        public int BestTrophyScore { get; set; } = 0;

        public string Faction { get; set; } = "rebel";
    }
}