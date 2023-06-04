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
    public class PreviousBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreviousBalancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(PreviousBalance previousBalance, int idVaultNote)
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
            var existingBalances = await _context.PreviousBalances
                .Where(a => a.IdVaultNote == vaultNote.Id)
                .ToListAsync();
            if (existingBalances.Count == 0)
            {
                // Создание записей в таблице Balances
                var foods = await _context.Foods.ToListAsync();
                foreach (var food in foods)
                {
                    var balances = new PreviousBalance
                    {
                        IdFood = food.Id,
                        IdVaultNote = vaultNote.Id,
                        StartBalance = 0,
                        EndBalance = 0
                    };

                    _context.Add(balances);
                }

                await _context.SaveChangesAsync();
            }

            // Получение списков Arrival и ProductConsumption для данной VaultNote
            var arrivals = await _context.Arrivals
                .Where(a => a.IdVaultNote == vaultNote.Id)
                .ToListAsync();

            var consumptions = await _context.ProductConsumptions
                .Where(c => c.IdVaultNote == vaultNote.Id)
                .ToListAsync();

            // Расчет конечного остатка на основе начального остатка, приходов и расходов
            foreach (var balance in existingBalances)
            {
                double startBalance = balance.StartBalance ?? 0;
                double totalArrival = arrivals.Where(a => a.IdFood == balance.IdFood)
                                              .Sum(a => a.FoodCount ?? 0);
                double totalConsumption = consumptions.Where(c => c.IdFood == balance.IdFood)
                                                      .Sum(c => (c.FoodCountChild ?? 0) + (c.FoodCountKid ?? 0));
                double endBalance = startBalance + totalArrival - totalConsumption;

                balance.EndBalance = endBalance;
                _context.Update(balance);
            }

            await _context.SaveChangesAsync();


            var balancess = await _context.PreviousBalances
            .Include(a => a.Food)
            .Where(a => a.IdVaultNote == vaultNote.Id)
            .ToListAsync();

            return View(balancess);
        }

        // GET: PreviousBalances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var previousBalance = await _context.PreviousBalances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (previousBalance == null)
            {
                return NotFound();
            }

            return View(previousBalance);
        }

        // GET: PreviousBalances/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreviousBalance previousBalance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(previousBalance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(previousBalance);
        }

        // GET: PreviousBalances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var previousBalance = await _context.PreviousBalances.FindAsync(id);
            if (previousBalance == null)
            {
                return NotFound();
            }
            return View(previousBalance);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreviousBalance previousBalance)
        {
            if (id != previousBalance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(previousBalance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreviousBalanceExists(previousBalance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { idVaultNote = previousBalance.IdVaultNote });
            }
            return View(previousBalance);
        }

        // GET: PreviousBalances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var previousBalance = await _context.PreviousBalances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (previousBalance == null)
            {
                return NotFound();
            }

            return View(previousBalance);
        }

        // POST: PreviousBalances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var previousBalance = await _context.PreviousBalances.FindAsync(id);
            _context.PreviousBalances.Remove(previousBalance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreviousBalanceExists(int id)
        {
            return _context.PreviousBalances.Any(e => e.Id == id);
        }
    }
}
