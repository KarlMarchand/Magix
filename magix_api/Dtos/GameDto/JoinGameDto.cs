namespace magix_api.Dtos.GameDto
{
    public class JoinGameDto
    {
        public string Type { get; set; } = string.Empty;
        public string? Mode { get; set; }
        public string? PrivateKey { get; set; }
    }
}
