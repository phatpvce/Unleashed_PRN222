using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unleashed_MVC.Models;

namespace Unleashed_MVC.Controllers
{
    public class ShippingMethodsController : Controller
    {
        private readonly UnleashedContext _context;

        public ShippingMethodsController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: ShippingMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShippingMethods.ToListAsync());
        }

        // GET: ShippingMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingMethod = await _context.ShippingMethods
                .FirstOrDefaultAsync(m => m.ShippingMethodId == id);
            if (shippingMethod == null)
            {
                return NotFound();
            }

            return View(shippingMethod);
        }

        // GET: ShippingMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShippingMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippingMethodId,ShippingMethodName")] ShippingMethod shippingMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shippingMethod);
        }

        // GET: ShippingMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingMethod = await _context.ShippingMethods.FindAsync(id);
            if (shippingMethod == null)
            {
                return NotFound();
            }
            return View(shippingMethod);
        }

        // POST: ShippingMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShippingMethodId,ShippingMethodName")] ShippingMethod shippingMethod)
        {
            if (id != shippingMethod.ShippingMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingMethodExists(shippingMethod.ShippingMethodId))
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
            return View(shippingMethod);
        }

        // GET: ShippingMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingMethod = await _context.ShippingMethods
                .FirstOrDefaultAsync(m => m.ShippingMethodId == id);
            if (shippingMethod == null)
            {
                return NotFound();
            }

            return View(shippingMethod);
        }

        // POST: ShippingMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingMethod = await _context.ShippingMethods.FindAsync(id);
            if (shippingMethod != null)
            {
                _context.ShippingMethods.Remove(shippingMethod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingMethodExists(int id)
        {
            return _context.ShippingMethods.Any(e => e.ShippingMethodId == id);
        }
    }
}
