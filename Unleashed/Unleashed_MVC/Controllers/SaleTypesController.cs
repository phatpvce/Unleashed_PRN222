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
    public class SaleTypesController : Controller
    {
        private readonly UnleashedContext _context;

        public SaleTypesController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: SaleTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleTypes.ToListAsync());
        }

        // GET: SaleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleType = await _context.SaleTypes
                .FirstOrDefaultAsync(m => m.SaleTypeId == id);
            if (saleType == null)
            {
                return NotFound();
            }

            return View(saleType);
        }

        // GET: SaleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SaleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleTypeId,SaleTypeName")] SaleType saleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleType);
        }

        // GET: SaleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleType = await _context.SaleTypes.FindAsync(id);
            if (saleType == null)
            {
                return NotFound();
            }
            return View(saleType);
        }

        // POST: SaleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleTypeId,SaleTypeName")] SaleType saleType)
        {
            if (id != saleType.SaleTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleTypeExists(saleType.SaleTypeId))
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
            return View(saleType);
        }

        // GET: SaleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleType = await _context.SaleTypes
                .FirstOrDefaultAsync(m => m.SaleTypeId == id);
            if (saleType == null)
            {
                return NotFound();
            }

            return View(saleType);
        }

        // POST: SaleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleType = await _context.SaleTypes.FindAsync(id);
            if (saleType != null)
            {
                _context.SaleTypes.Remove(saleType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleTypeExists(int id)
        {
            return _context.SaleTypes.Any(e => e.SaleTypeId == id);
        }
    }
}
