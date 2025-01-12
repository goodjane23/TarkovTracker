using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using TarkovTracker.Models;

namespace TarkovTracker.Controllers;
public class TarkovController : Controller
{
    List<UserRequirementsItem> finalItems = new();

    public TarkovController()
    {
        GetData().Wait();
    }

    public async Task<ActionResult> Index()
    {
        return View(finalItems);
    }

    public async Task GetData()
    {
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

        var hideoutStationLevels = hideout.Data.HideoutStations.SelectMany(x =>x.Levels);
        var items = hideoutStationLevels.SelectMany(x => x.ItemRequirements).ToList();
       
        foreach (var item in items)
        {
            var x = finalItems.FirstOrDefault(x => x.Item.Id == item.Item.Id);
            if (x != null)
            {
                x.Count += item.Count;
            }
            else
            {
                UserRequirementsItem userItem = new UserRequirementsItem 
                { 
                    Id = item.Id,
                    Item = item.Item,
                    Count = item.Count,
                    Quantity = item.Quantity,
                };
                finalItems.Add(userItem);
            }
        }
        finalItems = finalItems.OrderBy(x => x.Count).ToList();
    }

    [HttpGet]
    public async Task<ActionResult> DoMinusItem(string id)
    {
        var selectedItem = finalItems.FirstOrDefault(x => x.Id == id);
        if (selectedItem != null)
        {
            selectedItem.СollectedItemsCount -= 1;
        }

        return View("Index", finalItems);
    }

    [HttpGet]
    public async Task<ActionResult> DoPlusItem(string id)
    {
        var selectedItem = finalItems.FirstOrDefault(x => x.Id == id);
        if (selectedItem != null)
        {
            selectedItem.СollectedItemsCount += 1;
        }

        return View("Index", finalItems);
    }
}
