using magix_api.Services.GameOptionsService;

namespace magix_api.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MagixContext>();
                var gameOptions = services.GetRequiredService<IGameOptionsService>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context, gameOptions);
            }
        }
    }
}