using Microsoft.AspNetCore.Mvc;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers
{
    public class MenuItemsController : Controller
    {
        // Simulated in-memory data store
        private static List<MenuItemModel> _menuItems = new List<MenuItemModel>
        {
            new MenuItemModel
            {
                Id = 1,
                Name = "Coffee Bliss",
                Description = "Our finest single-source coffee, hand-picked and roasted to perfection.",
                Price = 5.00m,
                Image = "images/fem-coffee.jpg"
            },
            new MenuItemModel
            {
                Id = 2,
                Name = "Iced Coffee",
                Description = "Cold-brewed overnight with sugar and half-and-half.",
                Price = 5.00m,
                Image = "images/fem-iced-coffee.jpg"
            },
            new MenuItemModel
            {
                Id = 3,
                Name = "Latte Charm",
                Description = "Espresso with foamy milk and a variety of flavor options.",
                Price = 3.50m,
                Image = "images/fem-latte.jpg"
            },
            new MenuItemModel
            {
                Id = 4,
                Name = "Cappuccino",
                Description = "Evenly balanced milk and espresso, with optional toppings.",
                Price = 4.00m,
                Image = "images/fem-capuccino.jpg"
            },
            new MenuItemModel
            {
                Id = 5,
                Name = "Macchiato Marvel",
                Description = "Espresso with a dash of milk foam.",
                Price = 3.50m,
                Image = "images/fem-coffee-mac.jpg"
            }
        };

        public IActionResult Index()
        {
            return View(_menuItems);
        }

        public IActionResult Create()
        {
            return View();
        }

   [HttpPost]
public IActionResult Create(MenuItemModel item)
{
    if (item.ImageFile != null && item.ImageFile.Length > 0)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        Directory.CreateDirectory(uploadsFolder); 

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.ImageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            item.ImageFile.CopyTo(stream);
        }

        item.Image = "images/" + uniqueFileName;
    }

    item.Id = _menuItems.Max(m => m.Id) + 1;
    _menuItems.Add(item);

    return RedirectToAction("Index");
}

        public IActionResult Edit(int id)
        {
            var item = _menuItems.FirstOrDefault(m => m.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }
[HttpPost]
public IActionResult Edit(MenuItemModel updated)
{
    var item = _menuItems.FirstOrDefault(m => m.Id == updated.Id);
    if (item == null) return NotFound();

    item.Name = updated.Name;
    item.Description = updated.Description;
    item.Price = updated.Price;

    if (updated.ImageFile != null && updated.ImageFile.Length > 0)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(updated.ImageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            updated.ImageFile.CopyTo(stream);
        }

        item.Image = "images/" + uniqueFileName;
    }

    return RedirectToAction("Index");
}

        public IActionResult Delete(int id)
        {
            var item = _menuItems.FirstOrDefault(m => m.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = _menuItems.FirstOrDefault(m => m.Id == id);
            if (item != null) _menuItems.Remove(item);
            return RedirectToAction("Index");
        }
    }
}