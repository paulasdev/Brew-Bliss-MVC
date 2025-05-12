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
    public class ContactMessageController : Controller
    {
        private readonly BrewBlissDbContext _context;

        public ContactMessageController(BrewBlissDbContext context)
        {
            _context = context;
        }

        // GET: ContactMessage
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactMessages.ToListAsync());
        }

        // GET: ContactMessage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMessage = await _context.ContactMessages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactMessage == null)
            {
                return NotFound();
            }

            return View(contactMessage);
        }

        // GET: ContactMessage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactMessage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Message,SubmittedAt")] ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactMessage);
        }

        // GET: ContactMessage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMessage = await _context.ContactMessages.FindAsync(id);
            if (contactMessage == null)
            {
                return NotFound();
            }
            return View(contactMessage);
        }

        // POST: ContactMessage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Message,SubmittedAt")] ContactMessage contactMessage)
        {
            if (id != contactMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactMessageExists(contactMessage.Id))
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
            return View(contactMessage);
        }

        // GET: ContactMessage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactMessage = await _context.ContactMessages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactMessage == null)
            {
                return NotFound();
            }

            return View(contactMessage);
        }

        // POST: ContactMessage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactMessage = await _context.ContactMessages.FindAsync(id);
            if (contactMessage != null)
            {
                _context.ContactMessages.Remove(contactMessage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactMessageExists(int id)
        {
            return _context.ContactMessages.Any(e => e.Id == id);
        }
    }
}
