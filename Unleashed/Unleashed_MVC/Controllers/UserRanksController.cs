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
    public class UserRanksController : Controller
    {
        private readonly UnleashedContext _context;

        public UserRanksController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: UserRanks
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.UserRanks.Include(u => u.Rank).Include(u => u.User);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: UserRanks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRank = await _context.UserRanks
                .Include(u => u.Rank)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userRank == null)
            {
                return NotFound();
            }

            return View(userRank);
        }

        // GET: UserRanks/Create
        public IActionResult Create()
        {
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "RankId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserRanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RankId,MoneySpent,RankStatus,RankExpireDate,RankCreatedDate,RankUpdatedDate")] UserRank userRank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "RankId", userRank.RankId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userRank.UserId);
            return View(userRank);
        }

        // GET: UserRanks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRank = await _context.UserRanks.FindAsync(id);
            if (userRank == null)
            {
                return NotFound();
            }
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "RankId", userRank.RankId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userRank.UserId);
            return View(userRank);
        }

        // POST: UserRanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,RankId,MoneySpent,RankStatus,RankExpireDate,RankCreatedDate,RankUpdatedDate")] UserRank userRank)
        {
            if (id != userRank.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRankExists(userRank.UserId))
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
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "RankId", userRank.RankId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userRank.UserId);
            return View(userRank);
        }

        // GET: UserRanks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRank = await _context.UserRanks
                .Include(u => u.Rank)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userRank == null)
            {
                return NotFound();
            }

            return View(userRank);
        }

        // POST: UserRanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userRank = await _context.UserRanks.FindAsync(id);
            if (userRank != null)
            {
                _context.UserRanks.Remove(userRank);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRankExists(string id)
        {
            return _context.UserRanks.Any(e => e.UserId == id);
        }
    }
}
