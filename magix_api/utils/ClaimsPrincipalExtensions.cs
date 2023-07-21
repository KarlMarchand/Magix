using System.Security.Claims;

namespace magix_api.utils
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetPlayerId(this ClaimsPrincipal user)
        {
            return Convert.ToInt32(user.FindFirst("id")?.Value);
        }

        public static string GetPlayerKey(this ClaimsPrincipal user)
        {
            return user.FindFirst("key")?.Value ?? string.Empty;
        }
    }
}