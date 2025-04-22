using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers;

public class AboutController : Controller
{
    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger)
    {
        _logger = logger;
    }

    public IActionResult About()
    {
        return View();
    }

}
