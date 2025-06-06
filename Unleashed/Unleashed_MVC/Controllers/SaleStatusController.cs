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
    public class SaleStatusController : Controller
    {
        private readonly UnleashedContext _context;

        public SaleStatusController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: SaleStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleStatuses.ToListAsync());
        }

        // GET: SaleStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleStatus = await _context.SaleStatuses
                .FirstOrDefaultAsync(m => m.SaleStatusId == id);
            if (saleStatus == null)
            {
                return NotFound();
            }

            return View(saleStatus);
        }

        // GET: SaleStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SaleStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleStatusId,SaleStatusName")] SaleStatus saleStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleStatus);
        }

        // GET: SaleStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleStatus = await _context.SaleStatuses.FindAsync(id);
            if (saleStatus == null)
            {
                return NotFound();
            }
            return View(saleStatus);
        }

        // POST: SaleStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleStatusId,SaleStatusName")] SaleStatus saleStatus)
        {
            if (id != saleStatus.SaleStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleStatusExists(saleStatus.SaleStatusId))
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
            return View(saleStatus);
        }

        // GET: SaleStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleStatus = await _context.SaleStatuses
                .FirstOrDefaultAsync(m => m.SaleStatusId == id);
            if (saleStatus == null)
            {
                return NotFound();
            }

            return View(saleStatus);
        }

        // POST: SaleStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleStatus = await _context.SaleStatuses.FindAsync(id);
            if (saleStatus != null)
            {
                _context.SaleStatuses.Remove(saleStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleStatusExists(int id)
        {
            return _context.SaleStatuses.Any(e => e.SaleStatusId == id);
        }
    }
}
