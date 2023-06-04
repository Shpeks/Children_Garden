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
    public class ProductConsumptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductConsumptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductConsumptions
        public async Task<IActionResult> Index(int idVaultNote)
        {
            var vaultNote = await _context.VaultNotes
                .Include(v => v.Vault)
                .FirstOrDefaultAsync(v => v.Id == idVaultNote);

            if (vaultNote == null)
            {
                return NotFound();
            }
            int idVault = vaultNote.Vault.Id;
            ViewBag.IdVault = idVault;

            // Проверка наличия записи в таблице Product для данной VaultNote
            var existingProducts = await _context.ProductConsumptions
                .Where(a => a.IdVaultNote == vaultNote.Id)
                .ToListAsync();
            ViewBag.IdVaultNote = idVaultNote;
            if (existingProducts.Count == 0)
            {
                // Создание записей в таблице Arrival
                var foods = await _context.Foods.ToListAsync();
                foreach (var food in foods)
                {
                    var products = new ProductConsumption
                    {
                        Date = vaultNote.Date,
                        IdFood = food.Id,
                        IdVaultNote = vaultNote.Id,
                        FoodCountChild = 0,
                        FoodCountKid = 0
                    };

                    _context.Add(products);
                }

                await _context.SaveChangesAsync();
            }

            var product = await _context.ProductConsumptions
            .Include(a => a.Food)
            .Where(a => a.IdVaultNote == vaultNote.Id)
            .ToListAsync();

            return View(product);
        }

        // GET: ProductConsumptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productConsumption = await _context.ProductConsumptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productConsumption == null)
            {
                return NotFound();
            }

            return View(productConsumption);
        }

        // GET: ProductConsumptions/Create
        public IActionResult Create()
        {
            ViewData["IdChildHouse"] = new SelectList(_context.ChildHouses, "ChildHouseId", "ChildHouseId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductConsumption productConsumption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productConsumption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdChildHouse"] = new SelectList(_context.ChildHouses, "ChildHouseId", "ChildHouseId");
            return View(productConsumption);
        }

        // GET: ProductConsumptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productConsumption = await _context.ProductConsumptions.FindAsync(id);
            if (productConsumption == null)
            {
                return NotFound();
            }
            ViewData["IdChildHouse"] = new SelectList(_context.ChildHouses, "ChildHouseId", "ChildHouseId");
            return View(productConsumption);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductConsumption productConsumption)
        {
            if (id != productConsumption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productConsumption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductConsumptionExists(productConsumption.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                    return RedirectToAction("Index", new { idVaultNote = productConsumption.IdVaultNote });
            }
            
            return View(productConsumption);
        }

        // GET: ProductConsumptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productConsumption = await _context.ProductConsumptions
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productConsumption == null)
            {
                return NotFound();
            }

            return View(productConsumption);
        }

        // POST: ProductConsumptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productConsumption = await _context.ProductConsumptions.FindAsync(id);
            _context.ProductConsumptions.Remove(productConsumption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductConsumptionExists(int id)
        {
            return _context.ProductConsumptions.Any(e => e.Id == id);
        }
    }
}
