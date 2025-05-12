using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers
{
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly BrewBlissDbContext _context;

        public MenuController(ILogger<MenuController> logger, BrewBlissDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: /Menu/Menu
        public async Task<IActionResult> Menu()
        {
            var items = await _context.MenuItems.Include(m => m.Category).ToListAsync();
            return View(items);
        }
    }
}