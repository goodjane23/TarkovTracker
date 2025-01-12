using System.Text.Json;
using TarkovTracker.Models;

namespace TarkovTracker.Services;

public class TarkovApiService 
{
    public async Task<IEnumerable<ItemRequirements>> GetShelterItems()
    {
        var finalItems = new List<ItemRequirements>();
        var data = new Dictionary<string, string>()
        {
            {"query", "{hideoutStations " +
            "{ id name normalizedName levels " +
            "{ id level itemRequirements" +
            "{ id count quantity item { id normalizedName description image512pxLink}}" +
            "}}}"}
        };

        using var httpClient = new HttpClient();

        //Http response message
        var httpResponse = await httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", data);

        //Response content
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        var hideout = JsonSerializer.Deserialize<ApiResponse>(responseContent, options);

        //Пролучаем список всех ItemRequirements

        var hideoutStationLevels = hideout.Data.HideoutStations.SelectMany(x => x.Levels);
        var items = hideoutStationLevels.SelectMany(x => x.ItemRequirements).ToList();

        foreach (var item in items)
        {
            var existItem = finalItems.FirstOrDefault(x => x.Item.Id.Equals(item.Item.Id));
            if (existItem != null)
            {
                existItem.Count += item.Count;
            }
            else
            {
                finalItems.Add(item);
            };
        }
        return items;
    }
}
