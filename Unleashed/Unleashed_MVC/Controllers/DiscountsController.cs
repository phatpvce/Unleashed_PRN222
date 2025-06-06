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
    public class DiscountsController : Controller
    {
        private readonly UnleashedContext _context;

        public DiscountsController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: Discounts
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.Discounts.Include(d => d.DiscountRankRequirementNavigation).Include(d => d.DiscountStatus).Include(d => d.DiscountType);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: Discounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .Include(d => d.DiscountRankRequirementNavigation)
                .Include(d => d.DiscountStatus)
                .Include(d => d.DiscountType)
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Discounts/Create
        public IActionResult Create()
        {
            ViewData["DiscountRankRequirement"] = new SelectList(_context.Ranks, "RankId", "RankId");
            ViewData["DiscountStatusId"] = new SelectList(_context.DiscountStatuses, "DiscountStatusId", "DiscountStatusId");
            ViewData["DiscountTypeId"] = new SelectList(_context.DiscountTypes, "DiscountTypeId", "DiscountTypeId");
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscountId,DiscountStatusId,DiscountTypeId,DiscountCode,DiscountValue,DiscountDescription,DiscountRankRequirement,DiscountMinimumOrderValue,DiscountMaximumValue,DiscountUsageLimit,DiscountStartDate,DiscountEndDate,DiscountCreatedAt,DiscountUpdatedAt,DiscountUsageCount")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountRankRequirement"] = new SelectList(_context.Ranks, "RankId", "RankId", discount.DiscountRankRequirement);
            ViewData["DiscountStatusId"] = new SelectList(_context.DiscountStatuses, "DiscountStatusId", "DiscountStatusId", discount.DiscountStatusId);
            ViewData["DiscountTypeId"] = new SelectList(_context.DiscountTypes, "DiscountTypeId", "DiscountTypeId", discount.DiscountTypeId);
            return View(discount);
        }

        // GET: Discounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            ViewData["DiscountRankRequirement"] = new SelectList(_context.Ranks, "RankId", "RankId", discount.DiscountRankRequirement);
            ViewData["DiscountStatusId"] = new SelectList(_context.DiscountStatuses, "DiscountStatusId", "DiscountStatusId", discount.DiscountStatusId);
            ViewData["DiscountTypeId"] = new SelectList(_context.DiscountTypes, "DiscountTypeId", "DiscountTypeId", discount.DiscountTypeId);
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscountId,DiscountStatusId,DiscountTypeId,DiscountCode,DiscountValue,DiscountDescription,DiscountRankRequirement,DiscountMinimumOrderValue,DiscountMaximumValue,DiscountUsageLimit,DiscountStartDate,DiscountEndDate,DiscountCreatedAt,DiscountUpdatedAt,DiscountUsageCount")] Discount discount)
        {
            if (id != discount.DiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.DiscountId))
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
            ViewData["DiscountRankRequirement"] = new SelectList(_context.Ranks, "RankId", "RankId", discount.DiscountRankRequirement);
            ViewData["DiscountStatusId"] = new SelectList(_context.DiscountStatuses, "DiscountStatusId", "DiscountStatusId", discount.DiscountStatusId);
            ViewData["DiscountTypeId"] = new SelectList(_context.DiscountTypes, "DiscountTypeId", "DiscountTypeId", discount.DiscountTypeId);
            return View(discount);
        }

        // GET: Discounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts
                .Include(d => d.DiscountRankRequirementNavigation)
                .Include(d => d.DiscountStatus)
                .Include(d => d.DiscountType)
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscountExists(int id)
        {
            return _context.Discounts.Any(e => e.DiscountId == id);
        }
    }
}
