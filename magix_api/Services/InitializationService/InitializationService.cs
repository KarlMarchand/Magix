namespace magix_api.Services.InitializationService
{
    public class InitializationService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InitializationService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var deckService = scope.ServiceProvider.GetRequiredService<DeckService.IDeckService>();
            await deckService.GetAllOptions();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
