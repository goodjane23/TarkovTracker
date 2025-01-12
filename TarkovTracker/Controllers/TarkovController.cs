﻿using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> DoMinusItem(string id)
    {
        await trackerService.DecreaseCount(id);
        var items = await trackerService.GetAllItems();

        return View("Index", items);
    }

    [HttpGet]
    public async Task<ActionResult> DoPlusItem(string id)
    {
        await trackerService.IncreaseCount(id);
        var items = await trackerService.GetAllItems();

        return View("Index", items);
    }
}
