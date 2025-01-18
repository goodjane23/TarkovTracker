using Microsoft.EntityFrameworkCore;
using TarkovTracker.Data;
using TarkovTracker.Data.Entities;

namespace TarkovTracker.Services;

public class TrackerService
{
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    public TrackerService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<UserDataItem>> GetAllItems()
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var items = await dbContext.UserItems
            .AsNoTracking()
            .OrderBy(x => x.NormalizedName)
            .ToListAsync();
        
        return items;
    }

    public async Task<UserDataItem?> GetById(string id)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var item = await dbContext.UserItems
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        
        return item;
    }

    public async Task IncreaseCount(string id)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var item = await dbContext.UserItems
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        
        if (item == null)
            return;

        item.СollectedCount++;
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DecreaseCount(string id)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();

        var item = await dbContext.UserItems
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (item == null || item.СollectedCount == 0)
            return;

        item.СollectedCount--;
        
        await dbContext.SaveChangesAsync();
    }
}
