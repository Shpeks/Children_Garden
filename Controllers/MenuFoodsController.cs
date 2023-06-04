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
    public class MenuFoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuFoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuFoods
        public async Task<IActionResult> Index(int IdMenu)
        {
            ViewBag.IdMenu = IdMenu;
            var applicationDbContext = _context.MenuFoods.Include(m => m.Meal).Include(m => m.MealTime).Include(m => m.Menu).Include(m => m.Unit);
            float totalPerUnit = await applicationDbContext.SumAsync(m => m.CountPerUnit);
            ViewBag.TotalPerUnit = totalPerUnit;
            float totalSupply = await applicationDbContext.SumAsync(v => v.Supply);
            ViewBag.TotalSupply = totalSupply;

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuFoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuFood = await _context.MenuFoods
                .Include(m => m.Meal)
                .Include(m => m.MealTime)
                .Include(m => m.Menu)
                .Include(m => m.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuFood == null)
            {
                return NotFound();
            }

            return View(menuFood);
        }

        // GET: MenuFoods/Create
        public IActionResult Create(int IdMenu)
        {
            ViewBag.IdMenu = IdMenu;
            
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name");
            ViewData["MealTimeId"] = new SelectList(_context.MealTimes, "Id", "Name");
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name");
            return View(new MenuFood { MenuId = IdMenu }); // Установите MenuId равным IdMenu
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MenuFood menuFood, int IdMenu)
        {

            menuFood.MenuId = IdMenu;
            if (ModelState.IsValid)
            {

                _context.Add(menuFood);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { IdMenu = IdMenu });
            }

            return View(menuFood);  
        }

        // GET: MenuFoods/Edit/5
        public async Task<IActionResult> Edit(int? id, int IdMenu)
        {
            ViewBag.IdMenu = IdMenu;
            if (id == null)
            {
                return NotFound();
            }

            var menuFood = await _context.MenuFoods.FindAsync(id);
            if (menuFood == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Name", menuFood.MealId);
            ViewData["MealTimeId"] = new SelectList(_context.MealTimes, "Id", "Name", menuFood.MealTimeId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name", menuFood.MenuId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Name", menuFood.UnitId);
            return View(menuFood);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuFood menuFood)
        {
            if (id != menuFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuFoodExists(menuFood.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { IdMenu = menuFood.MenuId });
            }
            ViewData["MealId"] = new SelectList(_context.Meals, "Id", "Id", menuFood.MealId);
            ViewData["MealTimeId"] = new SelectList(_context.MealTimes, "Id", "Id", menuFood.MealTimeId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", menuFood.MenuId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Id", menuFood.UnitId);
            return View(menuFood);
        }

        // GET: MenuFoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var menuFood = await _context.MenuFoods
                .Include(m => m.Meal)
                .Include(m => m.MealTime)
                .Include(m => m.Menu)
                .Include(m => m.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuFood == null)
            {
                return NotFound();
            }

            return View(menuFood);
        }

        // POST: MenuFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int IdMenu)
        {
            var menuFood = await _context.MenuFoods.FindAsync(id);
            _context.MenuFoods.Remove(menuFood);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { IdMenu = menuFood.MenuId });
        }

        private bool MenuFoodExists(int id)
        {
            return _context.MenuFoods.Any(e => e.Id == id);
        }
    }
}
