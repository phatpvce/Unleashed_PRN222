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
    public class DiscountTypesController : Controller
    {
        private readonly UnleashedContext _context;

        public DiscountTypesController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: DiscountTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscountTypes.ToListAsync());
        }

        // GET: DiscountTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountType = await _context.DiscountTypes
                .FirstOrDefaultAsync(m => m.DiscountTypeId == id);
            if (discountType == null)
            {
                return NotFound();
            }

            return View(discountType);
        }

        // GET: DiscountTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscountTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscountTypeId,DiscountTypeName")] DiscountType discountType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountType);
        }

        // GET: DiscountTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountType = await _context.DiscountTypes.FindAsync(id);
            if (discountType == null)
            {
                return NotFound();
            }
            return View(discountType);
        }

        // POST: DiscountTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscountTypeId,DiscountTypeName")] DiscountType discountType)
        {
            if (id != discountType.DiscountTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountTypeExists(discountType.DiscountTypeId))
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
            return View(discountType);
        }

        // GET: DiscountTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountType = await _context.DiscountTypes
                .FirstOrDefaultAsync(m => m.DiscountTypeId == id);
            if (discountType == null)
            {
                return NotFound();
            }

            return View(discountType);
        }

        // POST: DiscountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountType = await _context.DiscountTypes.FindAsync(id);
            if (discountType != null)
            {
                _context.DiscountTypes.Remove(discountType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountTypeExists(int id)
        {
            return _context.DiscountTypes.Any(e => e.DiscountTypeId == id);
        }
    }
}
