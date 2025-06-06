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
    public class VariationSinglesController : Controller
    {
        private readonly UnleashedContext _context;

        public VariationSinglesController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: VariationSingles
        public async Task<IActionResult> Index()
        {
            return View(await _context.VariationSingles.ToListAsync());
        }

        // GET: VariationSingles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variationSingle = await _context.VariationSingles
                .FirstOrDefaultAsync(m => m.VariationSingleId == id);
            if (variationSingle == null)
            {
                return NotFound();
            }

            return View(variationSingle);
        }

        // GET: VariationSingles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VariationSingles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VariationSingleId,VariationSingleCode,IsVariationSingleBought")] VariationSingle variationSingle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variationSingle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(variationSingle);
        }

        // GET: VariationSingles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variationSingle = await _context.VariationSingles.FindAsync(id);
            if (variationSingle == null)
            {
                return NotFound();
            }
            return View(variationSingle);
        }

        // POST: VariationSingles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VariationSingleId,VariationSingleCode,IsVariationSingleBought")] VariationSingle variationSingle)
        {
            if (id != variationSingle.VariationSingleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variationSingle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariationSingleExists(variationSingle.VariationSingleId))
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
            return View(variationSingle);
        }

        // GET: VariationSingles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variationSingle = await _context.VariationSingles
                .FirstOrDefaultAsync(m => m.VariationSingleId == id);
            if (variationSingle == null)
            {
                return NotFound();
            }

            return View(variationSingle);
        }

        // POST: VariationSingles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variationSingle = await _context.VariationSingles.FindAsync(id);
            if (variationSingle != null)
            {
                _context.VariationSingles.Remove(variationSingle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariationSingleExists(int id)
        {
            return _context.VariationSingles.Any(e => e.VariationSingleId == id);
        }
    }
}
