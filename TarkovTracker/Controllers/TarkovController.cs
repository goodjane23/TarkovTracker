using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using TarkovTracker.Data.Entities;
using TarkovTracker.Models;

namespace TarkovTracker.Controllers;
public class TarkovController : Controller
{
    public TarkovController()
    {

    }

    public async Task<ActionResult> Index()
    {
        return View(finalItems);
    }

    //[HttpGet]
    //public async Task<ActionResult> DoMinusItem(string id)
    //{
    //    var selectedItem = finalItems.FirstOrDefault(x => x.Id == id);
    //    if (selectedItem != null)
    //    {
    //        if (selectedItem.Count != 0)
    //            selectedItem.СollectedItemsCount -= 1;
    //    }

    //    return View("Index", finalItems);
    //}

    //[HttpGet]
    //public async Task<ActionResult> DoPlusItem(string id)
    //{
    //    var selectedItem = finalItems.FirstOrDefault(x => x.Id == id);
    //    if (selectedItem != null)
    //    {
    //        selectedItem.СollectedItemsCount += 1;
    //    }

    //    return View("Index", finalItems);
    //}
}
