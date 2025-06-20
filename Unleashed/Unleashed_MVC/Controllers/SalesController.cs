﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unleashed_MVC.Models;

namespace Unleashed_MVC.Controllers
{
    public class SalesController : Controller
    {
        private readonly UnleashedContext _context;

        public SalesController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.Sales.Include(s => s.SaleStatus).Include(s => s.SaleType);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.SaleStatus)
                .Include(s => s.SaleType)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["SaleStatusId"] = new SelectList(_context.SaleStatuses, "SaleStatusId", "SaleStatusId");
            ViewData["SaleTypeId"] = new SelectList(_context.SaleTypes, "SaleTypeId", "SaleTypeId");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,SaleTypeId,SaleStatusId,SaleValue,SaleStartDate,SaleEndDate,SaleCreatedAt,SaleUpdatedAt")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaleStatusId"] = new SelectList(_context.SaleStatuses, "SaleStatusId", "SaleStatusId", sale.SaleStatusId);
            ViewData["SaleTypeId"] = new SelectList(_context.SaleTypes, "SaleTypeId", "SaleTypeId", sale.SaleTypeId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["SaleStatusId"] = new SelectList(_context.SaleStatuses, "SaleStatusId", "SaleStatusId", sale.SaleStatusId);
            ViewData["SaleTypeId"] = new SelectList(_context.SaleTypes, "SaleTypeId", "SaleTypeId", sale.SaleTypeId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,SaleTypeId,SaleStatusId,SaleValue,SaleStartDate,SaleEndDate,SaleCreatedAt,SaleUpdatedAt")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewData["SaleStatusId"] = new SelectList(_context.SaleStatuses, "SaleStatusId", "SaleStatusId", sale.SaleStatusId);
            ViewData["SaleTypeId"] = new SelectList(_context.SaleTypes, "SaleTypeId", "SaleTypeId", sale.SaleTypeId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.SaleStatus)
                .Include(s => s.SaleType)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.SaleId == id);
        }
    }
}
