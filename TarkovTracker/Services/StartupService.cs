
using Microsoft.EntityFrameworkCore;
using TarkovTracker.Data;
using TarkovTracker.Data.Entities;

namespace TarkovTracker.Services;

public class StartupService : IHostedService
{
    private readonly TarkovApiService tarkovApi;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    private readonly ILogger<StartupService> logger;

    public StartupService(
        TarkovApiService tarkovApi,
        IDbContextFactory<AppDbContext> dbContextFactory,
        ILogger<StartupService> logger)
    {
        this.tarkovApi = tarkovApi;
        this.dbContextFactory = dbContextFactory;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var items = await tarkovApi.GetShelterItems();

        var userDataItems = items.Select(x => new UserDataItem
        {
            Id = x.Id,
            Description = x.Item.Description,
            Quantity = x.Quantity,
            Image512pxLink = x.Item.Image512pxLink,
            NormalizedName = x.Item.NormalizedName,
            RequiredCount = x.Count
        });
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        if (await dbContext.UserItems.AnyAsync())
        {
            logger.LogInformation("UserItems are already in database");
            return;
        }
        
        await dbContext.UserItems.AddRangeAsync(userDataItems);
        var itemsCount = await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Added {itemsCount} items", itemsCount);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
       return Task.CompletedTask;
    }
}
