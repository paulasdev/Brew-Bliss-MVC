using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers;

public class MenuController : Controller
{
    private readonly ILogger<MenuController> _logger;

    public MenuController(ILogger<MenuController> logger)
    {
        _logger = logger;
    }

    public IActionResult Menu()
    {
        return View();
    }



}
