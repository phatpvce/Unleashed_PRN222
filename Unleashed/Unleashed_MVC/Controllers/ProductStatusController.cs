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
    public class ProductStatusController : Controller
    {
        private readonly UnleashedContext _context;

        public ProductStatusController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: ProductStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductStatuses.ToListAsync());
        }

        // GET: ProductStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productStatus = await _context.ProductStatuses
                .FirstOrDefaultAsync(m => m.ProductStatusId == id);
            if (productStatus == null)
            {
                return NotFound();
            }

            return View(productStatus);
        }

        // GET: ProductStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductStatusId,ProductStatusName")] ProductStatus productStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productStatus);
        }

        // GET: ProductStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productStatus = await _context.ProductStatuses.FindAsync(id);
            if (productStatus == null)
            {
                return NotFound();
            }
            return View(productStatus);
        }

        // POST: ProductStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductStatusId,ProductStatusName")] ProductStatus productStatus)
        {
            if (id != productStatus.ProductStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductStatusExists(productStatus.ProductStatusId))
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
            return View(productStatus);
        }

        // GET: ProductStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productStatus = await _context.ProductStatuses
                .FirstOrDefaultAsync(m => m.ProductStatusId == id);
            if (productStatus == null)
            {
                return NotFound();
            }

            return View(productStatus);
        }

        // POST: ProductStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productStatus = await _context.ProductStatuses.FindAsync(id);
            if (productStatus != null)
            {
                _context.ProductStatuses.Remove(productStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductStatusExists(int id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
