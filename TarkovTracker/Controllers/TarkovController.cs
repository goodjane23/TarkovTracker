using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using TarkovTracker.Controllers.Models;
using TarkovTracker.Data.Entities;
using TarkovTracker.Models;
using TarkovTracker.Services;

namespace TarkovTracker.Controllers;

public class TarkovController : Controller
{
    private readonly TrackerService trackerService;

    public TarkovController(TrackerService trackerService)
    {
        this.trackerService = trackerService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var items = await trackerService.GetAllItems();
        return View(items);
    }
    
    [HttpPost]
    public async Task<ActionResult> DecreaseCount(ChangeItemCountRequest request)
    {
        await trackerService.DecreaseCount(request.ItemId);
        var item = await trackerService.GetById(request.ItemId);

        return PartialView("_TrackerItem", item);
    }
    
    [HttpPost]
    public async Task<ActionResult> IncreaseCount(ChangeItemCountRequest request)
    {
        await trackerService.IncreaseCount(request.ItemId);
        var item = await trackerService.GetById(request.ItemId);

        return PartialView("_TrackerItem", item);
    }
}
