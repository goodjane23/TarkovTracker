using System.Text.Json;
using TarkovTracker.Models;

namespace TarkovTracker.Services;

public class TarkovApiService 
{
    private readonly IHttpClientFactory httpClientFactory;

    public TarkovApiService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }
    
    public async Task<IEnumerable<ItemRequirements>> GetShelterItems()
    {
        var finalItems = new List<ItemRequirements>();
        
        var data = new Dictionary<string, string>
        {
            {"query", "{hideoutStations(lang: ru) " +
            "{ id name normalizedName levels " +
            "{ id level itemRequirements" +
            "{ id count quantity item { id name description image512pxLink}}" +
            "}}}"}
        };

        using var httpClient = httpClientFactory.CreateClient();
        
        var httpResponse = await httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", data);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        var hideout = JsonSerializer.Deserialize<ApiResponse>(responseContent, options);
        
        var items = hideout.Data.HideoutStations
            .SelectMany(x => x.Levels)
            .SelectMany(x => x.ItemRequirements)
            .ToList();

        foreach (var item in items)
        {
            var existItem = finalItems.FirstOrDefault(x => x.Item.Name.Equals(item.Item.Name));
            
            if (existItem != null)
            {
                existItem.Count += item.Count;
            }
            else
            {
                finalItems.Add(item);
            }
        }
        
        return finalItems;
    }
}
