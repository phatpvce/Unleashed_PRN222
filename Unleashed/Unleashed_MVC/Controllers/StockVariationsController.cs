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
    public class StockVariationsController : Controller
    {
        private readonly UnleashedContext _context;

        public StockVariationsController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: StockVariations
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.StockVariations.Include(s => s.Stock).Include(s => s.Variation);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: StockVariations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockVariation = await _context.StockVariations
                .Include(s => s.Stock)
                .Include(s => s.Variation)
                .FirstOrDefaultAsync(m => m.VariationId == id);
            if (stockVariation == null)
            {
                return NotFound();
            }

            return View(stockVariation);
        }

        // GET: StockVariations/Create
        public IActionResult Create()
        {
            ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId");
            ViewData["VariationId"] = new SelectList(_context.Variations, "VariationId", "VariationId");
            return View();
        }

        // POST: StockVariations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VariationId,StockId,StockQuantity")] StockVariation stockVariation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockVariation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId", stockVariation.StockId);
            ViewData["VariationId"] = new SelectList(_context.Variations, "VariationId", "VariationId", stockVariation.VariationId);
            return View(stockVariation);
        }

        // GET: StockVariations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockVariation = await _context.StockVariations.FindAsync(id);
            if (stockVariation == null)
            {
                return NotFound();
            }
            ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId", stockVariation.StockId);
            ViewData["VariationId"] = new SelectList(_context.Variations, "VariationId", "VariationId", stockVariation.VariationId);
            return View(stockVariation);
        }

        // POST: StockVariations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VariationId,StockId,StockQuantity")] StockVariation stockVariation)
        {
            if (id != stockVariation.VariationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockVariation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockVariationExists(stockVariation.VariationId))
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
            ViewData["StockId"] = new SelectList(_context.Stocks, "StockId", "StockId", stockVariation.StockId);
            ViewData["VariationId"] = new SelectList(_context.Variations, "VariationId", "VariationId", stockVariation.VariationId);
            return View(stockVariation);
        }

        // GET: StockVariations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockVariation = await _context.StockVariations
                .Include(s => s.Stock)
                .Include(s => s.Variation)
                .FirstOrDefaultAsync(m => m.VariationId == id);
            if (stockVariation == null)
            {
                return NotFound();
            }

            return View(stockVariation);
        }

        // POST: StockVariations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockVariation = await _context.StockVariations.FindAsync(id);
            if (stockVariation != null)
            {
                _context.StockVariations.Remove(stockVariation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockVariationExists(int id)
        {
            return _context.StockVariations.Any(e => e.VariationId == id);
        }
    }
}
