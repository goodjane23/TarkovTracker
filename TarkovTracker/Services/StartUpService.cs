
namespace TarkovTracker.Services;

public class StartUpService : IHostedService
{
    private readonly TarkovApiService tarkovApi;

    public StartUpService(TarkovApiService tarkovApi)
    {
        this.tarkovApi = tarkovApi;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await tarkovApi.GetShelterItems();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
       return Task.CompletedTask;
    }
}
