namespace magix_api.Services.AuthentificationService
{
    public interface IAuthentificationService
    {
        string GenerateJwtToken(Player player);
    }
}