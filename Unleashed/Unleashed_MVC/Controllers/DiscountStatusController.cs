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
    public class DiscountStatusController : Controller
    {
        private readonly UnleashedContext _context;

        public DiscountStatusController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: DiscountStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscountStatuses.ToListAsync());
        }

        // GET: DiscountStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountStatus = await _context.DiscountStatuses
                .FirstOrDefaultAsync(m => m.DiscountStatusId == id);
            if (discountStatus == null)
            {
                return NotFound();
            }

            return View(discountStatus);
        }

        // GET: DiscountStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscountStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscountStatusId,DiscountStatusName")] DiscountStatus discountStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discountStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discountStatus);
        }

        // GET: DiscountStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountStatus = await _context.DiscountStatuses.FindAsync(id);
            if (discountStatus == null)
            {
                return NotFound();
            }
            return View(discountStatus);
        }

        // POST: DiscountStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscountStatusId,DiscountStatusName")] DiscountStatus discountStatus)
        {
            if (id != discountStatus.DiscountStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discountStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountStatusExists(discountStatus.DiscountStatusId))
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
            return View(discountStatus);
        }

        // GET: DiscountStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discountStatus = await _context.DiscountStatuses
                .FirstOrDefaultAsync(m => m.DiscountStatusId == id);
            if (discountStatus == null)
            {
                return NotFound();
            }

            return View(discountStatus);
        }

        // POST: DiscountStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discountStatus = await _context.DiscountStatuses.FindAsync(id);
            if (discountStatus != null)
            {
                _context.DiscountStatuses.Remove(discountStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountStatusExists(int id)
        {
            return _context.DiscountStatuses.Any(e => e.DiscountStatusId == id);
        }
    }
}
