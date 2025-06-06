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
    public class NotificationUsersController : Controller
    {
        private readonly UnleashedContext _context;

        public NotificationUsersController(UnleashedContext context)
        {
            _context = context;
        }

        // GET: NotificationUsers
        public async Task<IActionResult> Index()
        {
            var unleashedContext = _context.NotificationUsers.Include(n => n.Notification).Include(n => n.User);
            return View(await unleashedContext.ToListAsync());
        }

        // GET: NotificationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationUser = await _context.NotificationUsers
                .Include(n => n.Notification)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NotificationId == id);
            if (notificationUser == null)
            {
                return NotFound();
            }

            return View(notificationUser);
        }

        // GET: NotificationUsers/Create
        public IActionResult Create()
        {
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "NotificationId", "NotificationId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: NotificationUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationId,UserId,IsNotificationViewed,IsNotificationDeleted")] NotificationUser notificationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notificationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "NotificationId", "NotificationId", notificationUser.NotificationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", notificationUser.UserId);
            return View(notificationUser);
        }

        // GET: NotificationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationUser = await _context.NotificationUsers.FindAsync(id);
            if (notificationUser == null)
            {
                return NotFound();
            }
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "NotificationId", "NotificationId", notificationUser.NotificationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", notificationUser.UserId);
            return View(notificationUser);
        }

        // POST: NotificationUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotificationId,UserId,IsNotificationViewed,IsNotificationDeleted")] NotificationUser notificationUser)
        {
            if (id != notificationUser.NotificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationUserExists(notificationUser.NotificationId))
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
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "NotificationId", "NotificationId", notificationUser.NotificationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", notificationUser.UserId);
            return View(notificationUser);
        }

        // GET: NotificationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationUser = await _context.NotificationUsers
                .Include(n => n.Notification)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NotificationId == id);
            if (notificationUser == null)
            {
                return NotFound();
            }

            return View(notificationUser);
        }

        // POST: NotificationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificationUser = await _context.NotificationUsers.FindAsync(id);
            if (notificationUser != null)
            {
                _context.NotificationUsers.Remove(notificationUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationUserExists(int id)
        {
            return _context.NotificationUsers.Any(e => e.NotificationId == id);
        }
    }
}
