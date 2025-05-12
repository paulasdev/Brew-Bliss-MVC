using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrewBlissApp.Models;

namespace BrewBlissApp.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly BrewBlissDbContext _context;

        public MenuItemController(BrewBlissDbContext context)
        {
            _context = context;
        }

        // GET: MenuItem
        public async Task<IActionResult> Index()
        {
            var brewBlissDbContext = _context.MenuItems.Include(m => m.Category);
            return View(await brewBlissDbContext.ToListAsync());
        }

        // GET: MenuItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItem/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: MenuItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuItemId,Name,Description,Price,ImagePath,CategoryId,ImageData")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", menuItem.CategoryId);
            return View(menuItem);
        }

        // GET: MenuItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", menuItem.CategoryId);
            return View(menuItem);
        }

        // POST: MenuItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuItemId,Name,Description,Price,ImagePath,CategoryId,ImageData")] MenuItem menuItem)
        {
            if (id != menuItem.MenuItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.MenuItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", menuItem.CategoryId);
            return View(menuItem);
        }

        // GET: MenuItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuItemId == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.MenuItemId == id);
        }
    }
}
