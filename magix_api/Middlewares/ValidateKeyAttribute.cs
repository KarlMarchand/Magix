using magix_api.utils;
using Microsoft.AspNetCore.Authorization;

namespace magix_api.Middlewares
{
    public class ValidateKeyRequirement : IAuthorizationRequirement
    {

    }

    public class ValidateKeyHandler : AuthorizationHandler<ValidateKeyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ValidateKeyRequirement requirement)
        {
            var keyClaim = context.User.FindFirst(c => c.Type == "key")?.Value;

            if (string.IsNullOrEmpty(keyClaim))
            {
                return;
            }

            if (!await Validate(keyClaim))
            {
                return;
            }

            context.Succeed(requirement);
        }

        private async Task<bool> Validate(string key)
        {
            ServerResponse<string>? response = await GameServerAPI.CallApi<string>("check-key", new Dictionary<string, string>() { { "key", key } });
            return response.Content != null && response.Content.Equals("VALID_KEY");
        }
    }
}