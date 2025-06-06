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
    public class UserDiscountsController : Controller
    {
        private readonly UnleashedContext _context;

        public UserDiscountsController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: UserDiscounts
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.UserDiscounts.Include(u => u.Discount).Include(u => u.User);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: UserDiscounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDiscount = await _context.UserDiscounts
                .Include(u => u.Discount)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDiscount == null)
            {
                return NotFound();
            }

            return View(userDiscount);
        }

        // GET: UserDiscounts/Create
        public IActionResult Create()
        {
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,DiscountId,IsDiscountUsed,DiscountUsedAt")] UserDiscount userDiscount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDiscount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId", userDiscount.DiscountId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userDiscount.UserId);
            return View(userDiscount);
        }

        // GET: UserDiscounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDiscount = await _context.UserDiscounts.FindAsync(id);
            if (userDiscount == null)
            {
                return NotFound();
            }
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId", userDiscount.DiscountId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userDiscount.UserId);
            return View(userDiscount);
        }

        // POST: UserDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,DiscountId,IsDiscountUsed,DiscountUsedAt")] UserDiscount userDiscount)
        {
            if (id != userDiscount.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDiscount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDiscountExists(userDiscount.UserId))
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
            ViewData["DiscountId"] = new SelectList(_context.Discounts, "DiscountId", "DiscountId", userDiscount.DiscountId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userDiscount.UserId);
            return View(userDiscount);
        }

        // GET: UserDiscounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDiscount = await _context.UserDiscounts
                .Include(u => u.Discount)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDiscount == null)
            {
                return NotFound();
            }

            return View(userDiscount);
        }

        // POST: UserDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userDiscount = await _context.UserDiscounts.FindAsync(id);
            if (userDiscount != null)
            {
                _context.UserDiscounts.Remove(userDiscount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDiscountExists(string id)
        {
            return _context.UserDiscounts.Any(e => e.UserId == id);
        }
    }
}
