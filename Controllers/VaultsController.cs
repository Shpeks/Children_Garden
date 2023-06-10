using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Data;
using Diplom.Models;
using Microsoft.AspNetCore.Identity;

namespace Diplom.Controllers
{
    public class VaultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public VaultsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        
        public async Task<IActionResult> Index(int idVaults)
        {
            ViewBag.IdVaults = idVaults;
            var vaults = await _context.Vaults.Include(v => v.ApplicationUsers).ToListAsync();
            return View(vaults);
        }

        
        public async Task<IActionResult> Create()
        {
            ViewData["IdUser"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            var fabricatorRoles = await _userManager.GetUsersInRoleAsync("Fabricator");
            var users = fabricatorRoles.Select(s => new SelectOptions { value = s.Id, Fn = s.FirstName, Ln = s.LastName }).ToList();
            TempData["users"] = users;
            return View();
        }

        public record SelectOptions
        {
            public string Fn { get; set; }
            public string Ln { get; set; }
            public string value { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vault vault)
        {
            if (ModelState.IsValid)
            {
                var minDateRange = 10; // Минимальный отрезок времени (в днях)
                var dateRange = (vault.DateEnd.Date - vault.DateStart.Date).TotalDays;

                if (dateRange <= minDateRange)
                {
                    ModelState.AddModelError(string.Empty, $"Выбранный отрезок времени должен быть не менее {minDateRange} дней или ошибка в выбранных датах.");
                    await SetCreateViewData();
                    return View(vault);
                }

                _context.Add(vault);
                await _context.SaveChangesAsync();

                var currentDate = vault.DateStart;
                var endDate = vault.DateEnd;

                while (currentDate <= endDate)
                {
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var vaultNote = new VaultNote
                        {
                            Date = currentDate,
                            IdVault = vault.Id
                        };
                        _context.Add(vaultNote);

                        var foods = await _context.Foods.ToListAsync();
                        foreach (var food in foods)
                        {
                            var balances = new PreviousBalance
                            {
                                IdFood = food.Id,
                                VaultNote = vaultNote,
                                StartBalance = 0,
                                EndBalance = 0
                            };

                            _context.Add(balances);
                        }
                    }

                    currentDate = currentDate.AddDays(1);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await SetCreateViewData();
            return View(vault);
        }

        // GET: Vaults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            await SetEditViewData();

            if (id == null)
            {
                return NotFound();
            }

            var vault = await _context.Vaults.FindAsync(id);
            if (vault == null)
            {
                return NotFound();
            }

            return View(vault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vault vault)
        {
            if (id != vault.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var minDateRange = 10; // Минимальный отрезок времени (в днях)
                var dateRange = (vault.DateEnd.Date - vault.DateStart.Date).TotalDays;

                if (dateRange < minDateRange)
                {
                    ModelState.AddModelError(string.Empty, $"Выбранный отрезок времени должен быть не менее {minDateRange} дней или ошибка в выбранных датах.");
                    await SetEditViewData();
                    return View(vault);
                }

                try
                {
                    _context.Update(vault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaultExists(vault.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var currentDate = vault.DateStart;
                var endDate = vault.DateEnd;

                while (currentDate <= endDate)
                {
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var vaultNote = new VaultNote
                        {
                            Date = currentDate,
                            IdVault = vault.Id
                        };

                        _context.Update(vaultNote);
                    }

                    currentDate = currentDate.AddDays(1);
                }

                return RedirectToAction("Index", "Vaults", new { idVaults = id });
            }

            await SetEditViewData();
            return View(vault);
        }

        // GET: Vaults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vault = await _context.Vaults.Include(v => v.ApplicationUsers).FirstOrDefaultAsync(m => m.Id == id);
            if (vault == null)
            {
                return NotFound();
            }

            return View(vault);
        }

        // POST: Vaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int idVault)
        {
            ViewBag.IdVault = idVault;
            var vault = await _context.Vaults.FindAsync(id);
            _context.Vaults.Remove(vault);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task SetCreateViewData()
        {
            ViewData["IdUser"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            var fabricatorRoles = await _userManager.GetUsersInRoleAsync("Fabricator");
            var users = fabricatorRoles.Select(s => new SelectOptions { value = s.Id, Fn = s.FirstName, Ln = s.LastName }).ToList();
            TempData["users"] = users;
        }

        private async Task SetEditViewData()
        {
            var fabricatorRoles = await _userManager.GetUsersInRoleAsync("Fabricator");
            var users = fabricatorRoles.Select(s => new SelectOptions { value = s.Id, Fn = s.FirstName, Ln = s.LastName }).ToList();
            TempData["users"] = users;
        }
            
        private bool VaultExists(int id)
        {
            return _context.Vaults.Any(e => e.Id == id);
        }
    }
}