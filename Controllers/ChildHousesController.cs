using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Data;
using Diplom.Models;

namespace Diplom.Controllers
{
    public class ChildHousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildHousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChildHouses
        public async Task<IActionResult> Index(int idVaultNote)
        {
            ViewBag.IdVaultNote = idVaultNote;
            
            bool hasRecords = await _context.ChildHouses.AnyAsync(ch => ch.IdVaultNote == idVaultNote);

            if (!hasRecords)
            {
                ChildHouse childHouse1 = new ChildHouse
                {
                    NameHouse = "Ясли",
                    ChildCount = 0,
                    IdVaultNote = idVaultNote
                };
                _context.ChildHouses.Add(childHouse1);

                ChildHouse childHouse2 = new ChildHouse
                {
                    NameHouse = "Сад",
                    ChildCount = 0,
                    IdVaultNote = idVaultNote
                };
                _context.ChildHouses.Add(childHouse2);

                await _context.SaveChangesAsync();
            }
            var childHouses = await _context.ChildHouses
            .Where(ch => ch.IdVaultNote == idVaultNote)
            .ToListAsync();
            return View(childHouses);
        }

        // GET: ChildHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childHouse = await _context.ChildHouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childHouse == null)
            {
                return NotFound();
            }

            return View(childHouse);
        }

        // GET: ChildHouses/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ChildHouse childHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(childHouse);
        }

        // GET: ChildHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childHouse = await _context.ChildHouses.FindAsync(id);
            if (childHouse == null)
            {
                return NotFound();
            }
            return View(childHouse);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChildHouse childHouse)
        {
            if (id != childHouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildHouseExists(childHouse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { idVaultNote = childHouse.IdVaultNote });
            }
            return View(childHouse);
        }

        // GET: ChildHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var childHouse = await _context.ChildHouses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (childHouse == null)
            {
                return NotFound();
            }

            return View(childHouse);
        }

        // POST: ChildHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var childHouse = await _context.ChildHouses.FindAsync(id);
            _context.ChildHouses.Remove(childHouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildHouseExists(int id)
        {
            return _context.ChildHouses.Any(e => e.Id == id);
        }
    }
}
