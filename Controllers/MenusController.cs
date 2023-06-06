using System;
using System.Collections.Generic;
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
    public class MenusController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MenusController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public record SelectOptions
        {
            public string Fn { get; set; }
            public string Ln { get; set; }
            public string value { get; set; }
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Menus.Include(m => m.ApplicationUsers);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.ApplicationUsers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public async Task<IActionResult> Create()
        {
            var roles = await _userManager.GetUsersInRoleAsync("Medic");
            var users = roles
                .Select(s => new SelectOptions
                {
                    value = s.Id,
                    Fn = s.FirstName,
                    Ln = s.LastName
                })
                .ToList();
            TempData["users"] = users;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUser"] = new SelectList(_context.ApplicationUsers, "Id", "Id", menu.IdUser);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }


            var roles = await _userManager.GetUsersInRoleAsync("Medic");
            var users = roles
                .Select(s => new SelectOptions
                {
                    value = s.Id,
                    Fn = s.FirstName,
                    Ln = s.LastName
                })
                .ToList();
            TempData["users"] = users;
            ViewData["IdUser"] = new SelectList(_context.ApplicationUsers, "Id", "Id", menu.IdUser);
            return View(menu);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                
                if (ModelState.IsValid)
                {
                    _context.Update(menu);

                    
                    var menuFoods = await _context.MenuFoods.Where(mf => mf.MenuId == menu.Id).ToListAsync();
                    foreach (var menuFood in menuFoods)
                    {
                        menuFood.Supply = menuFood.CountPerUnit * menu.ChildCount/1000;
                        _context.Update(menuFood);
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["IdUser"] = new SelectList(_context.ApplicationUsers, "Id", "Id", menu.IdUser);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.ApplicationUsers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
