using magix_api.Services.GameOptionsService;

namespace magix_api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MagixContext context, IGameOptionsService gameOptions)
        {

            if (context.Factions.Any())
            {
                return;   // DB has been seeded
            }
            
            gameOptions.GetAllOptions();

            context.SaveChanges();
        }
    }
}