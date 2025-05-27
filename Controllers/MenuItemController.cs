using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrewBlissApp.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace BrewBlissApp.Controllers
{
[Authorize]
public class MenuItemController : Controller
    {
        private readonly BrewBlissDbContext _context;

        public MenuItemController(BrewBlissDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var brewBlissDbContext = _context.MenuItems.Include(m => m.Category);
            return View(await brewBlissDbContext.ToListAsync());
        }

 public async Task<IActionResult> Details(int? id)
{
    if (id == null)
        return NotFound();

    var menuItem = await _context.MenuItems
        .Include(m => m.Category)
        .FirstOrDefaultAsync(m => m.MenuItemId == id);

    if (menuItem == null)
        return NotFound();

    using var httpClient = new HttpClient();

    try
    {
        // Encode the name for URL safety
        var detailApi = $"http://localhost:5120/api/GetMenuItem/{Uri.EscapeDataString(menuItem.Name)}";
var detailResponse = await httpClient.GetAsync(detailApi);
if (detailResponse.IsSuccessStatusCode)
{
    var detailJson = await detailResponse.Content.ReadFromJsonAsync<JsonElement>();
    ViewBag.Ingredients = detailJson.GetProperty("ingredients").GetString();
    ViewBag.Calories = detailJson.GetProperty("calories").GetInt32();
    ViewBag.Vegan = detailJson.GetProperty("vegan").GetBoolean();
}
        else
        {
            Console.WriteLine($"Detail API failed: {detailResponse.StatusCode}");
        }

        // Call CategoryInfo API
        var categoryName = menuItem.Category?.CategoryName;
        if (!string.IsNullOrEmpty(categoryName))
        {
            var encodedCategory = Uri.EscapeDataString(categoryName);
            var categoryApi = $"http://localhost:5120/api/CategoryInfo/{encodedCategory}";
            Console.WriteLine($"Calling CategoryInfo API: {categoryApi}");

            var categoryResponse = await httpClient.GetAsync(categoryApi);
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryInfo = await categoryResponse.Content.ReadFromJsonAsync<dynamic>();
                ViewBag.CategoryInfoName = categoryInfo?.categoryInfoName ?? "Unknown";
            }
            else
            {
                Console.WriteLine($"Category API failed: {categoryResponse.StatusCode}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"API error: {ex.Message}");
        ViewBag.ApiError = "Failed to fetch extra info.";
    }

    return View(menuItem);
}
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (menuItem.ImageFile != null)
            {
                try
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(menuItem.ImageFile.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItem.ImageFile.CopyToAsync(stream);
                    }

                    menuItem.ImagePath = "/images/" + fileName;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while uploading the image.", ex);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(menuItem);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Item added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error: Could not save menu item.", ex);
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", menuItem.CategoryId);
            return View(menuItem);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
                return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", menuItem.CategoryId);
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem)
        {
            if (id != menuItem.MenuItemId)
                return NotFound();

            if (menuItem.ImageFile != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(menuItem.ImageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await menuItem.ImageFile.CopyToAsync(stream);
                }

                menuItem.ImagePath = "/images/" + fileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Item updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.MenuItemId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", menuItem.CategoryId);
            return View(menuItem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var menuItem = await _context.MenuItems.Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);

            if (menuItem == null)
                return NotFound();

            return View(menuItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
                throw new Exception($"Menu item with ID {id} was not found for deletion.");

            try
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Item deleted successfully!";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete menu item.", ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.MenuItemId == id);
        }
    }
}