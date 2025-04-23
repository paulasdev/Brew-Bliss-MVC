using Microsoft.AspNetCore.Mvc;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers
{
    public class MenuItemsController : Controller
    {
        public IActionResult Index()
        {
            var menuItems = new List<MenuItemModel>
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

            return View(menuItems);
        }
    }
}