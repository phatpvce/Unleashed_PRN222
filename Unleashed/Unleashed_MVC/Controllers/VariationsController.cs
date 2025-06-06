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
    public class VariationsController : Controller
    {
        private readonly UnleashedContext _context;

        public VariationsController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: Variations
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.Variations.Include(v => v.Color).Include(v => v.Product).Include(v => v.Size);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: Variations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variations
                .Include(v => v.Color)
                .Include(v => v.Product)
                .Include(v => v.Size)
                .FirstOrDefaultAsync(m => m.VariationId == id);
            if (variation == null)
            {
                return NotFound();
            }

            return View(variation);
        }

        // GET: Variations/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
            return View();
        }

        // POST: Variations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VariationId,ProductId,SizeId,ColorId,VariationImage,VariationPrice")] Variation variation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", variation.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", variation.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", variation.SizeId);
            return View(variation);
        }

        // GET: Variations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variations.FindAsync(id);
            if (variation == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", variation.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", variation.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", variation.SizeId);
            return View(variation);
        }

        // POST: Variations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VariationId,ProductId,SizeId,ColorId,VariationImage,VariationPrice")] Variation variation)
        {
            if (id != variation.VariationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariationExists(variation.VariationId))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", variation.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", variation.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId", variation.SizeId);
            return View(variation);
        }

        // GET: Variations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variation = await _context.Variations
                .Include(v => v.Color)
                .Include(v => v.Product)
                .Include(v => v.Size)
                .FirstOrDefaultAsync(m => m.VariationId == id);
            if (variation == null)
            {
                return NotFound();
            }

            return View(variation);
        }

        // POST: Variations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variation = await _context.Variations.FindAsync(id);
            if (variation != null)
            {
                _context.Variations.Remove(variation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariationExists(int id)
        {
            return _context.Variations.Any(e => e.VariationId == id);
        }
    }
}
