using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
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
    
    [HttpGet]
    public async Task<ActionResult> DecreasePartial([FromQuery] string id)
    {
        await trackerService.DecreaseCount(id);
        var item = await trackerService.GetById(id);

        return PartialView("_TrackerItem", item);
    }
    
    [HttpGet]
    public async Task<ActionResult> IncreasePartial([FromQuery] string id)
    {
        await trackerService.IncreaseCount(id);
        var item = await trackerService.GetById(id);

        return PartialView("_TrackerItem", item);
    }
}
