using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using magix_api.utils;

namespace magix_api.Middlewares
{
    public class ValidateKeyAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? contentType = context.HttpContext.Request.ContentType;
            if (contentType == null || !contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestResult();
                return;
            }

            context.HttpContext.Request.Body.Position = 0;
            var requestBody = await JsonSerializer.DeserializeAsync<KeyRequest>(context.HttpContext.Request.Body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (requestBody is null || string.IsNullOrEmpty(requestBody.Key))
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (!await Validate(requestBody.Key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }

        private async Task<bool> Validate(string key)
        {
            string? response = await GameServerAPI.CallApi<string>("check-key", new Dictionary<string, string>() { { "key", key } });
            return response != null && response.Equals("VALID_KEY");
        }
    }
    public class KeyRequest
    {
        public string Key { get; set; }
    }

}