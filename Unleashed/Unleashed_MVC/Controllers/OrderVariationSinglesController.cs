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
    public class OrderVariationSinglesController : Controller
    {
        private readonly UnleashedContext _context;

        public OrderVariationSinglesController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: OrderVariationSingles
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.OrderVariationSingles.Include(o => o.Order).Include(o => o.VariationSingle);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: OrderVariationSingles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderVariationSingle = await _context.OrderVariationSingles
                .Include(o => o.Order)
                .Include(o => o.VariationSingle)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderVariationSingle == null)
            {
                return NotFound();
            }

            return View(orderVariationSingle);
        }

        // GET: OrderVariationSingles/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["VariationSingleId"] = new SelectList(_context.VariationSingles, "VariationSingleId", "VariationSingleId");
            return View();
        }

        // POST: OrderVariationSingles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,VariationSingleId,SaleId,VariationPriceAtPurchase")] OrderVariationSingle orderVariationSingle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderVariationSingle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderVariationSingle.OrderId);
            ViewData["VariationSingleId"] = new SelectList(_context.VariationSingles, "VariationSingleId", "VariationSingleId", orderVariationSingle.VariationSingleId);
            return View(orderVariationSingle);
        }

        // GET: OrderVariationSingles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderVariationSingle = await _context.OrderVariationSingles.FindAsync(id);
            if (orderVariationSingle == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderVariationSingle.OrderId);
            ViewData["VariationSingleId"] = new SelectList(_context.VariationSingles, "VariationSingleId", "VariationSingleId", orderVariationSingle.VariationSingleId);
            return View(orderVariationSingle);
        }

        // POST: OrderVariationSingles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderId,VariationSingleId,SaleId,VariationPriceAtPurchase")] OrderVariationSingle orderVariationSingle)
        {
            if (id != orderVariationSingle.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderVariationSingle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderVariationSingleExists(orderVariationSingle.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderVariationSingle.OrderId);
            ViewData["VariationSingleId"] = new SelectList(_context.VariationSingles, "VariationSingleId", "VariationSingleId", orderVariationSingle.VariationSingleId);
            return View(orderVariationSingle);
        }

        // GET: OrderVariationSingles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderVariationSingle = await _context.OrderVariationSingles
                .Include(o => o.Order)
                .Include(o => o.VariationSingle)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderVariationSingle == null)
            {
                return NotFound();
            }

            return View(orderVariationSingle);
        }

        // POST: OrderVariationSingles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderVariationSingle = await _context.OrderVariationSingles.FindAsync(id);
            if (orderVariationSingle != null)
            {
                _context.OrderVariationSingles.Remove(orderVariationSingle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderVariationSingleExists(string id)
        {
            return _context.OrderVariationSingles.Any(e => e.OrderId == id);
        }
    }
}
