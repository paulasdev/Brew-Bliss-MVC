using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    public IActionResult Contact()
    {
        return View();
    }
}
