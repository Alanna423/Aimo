using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aimo.Data;
using Aimo.Models;

namespace Aimo.Controllers
{
    public class ExploreContoller : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExploreContoller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExploreContoller
        public async Task<IActionResult> Index()
        {
              return _context.Explore != null ? 
                          View(await _context.Explore.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Explore'  is null.");
        }

        // GET: ExploreContoller/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Explore == null)
            {
                return NotFound();
            }

            var explore = await _context.Explore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (explore == null)
            {
                return NotFound();
            }

            return View(explore);
        }

        // GET: ExploreContoller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExploreContoller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Requester,Accepted_By,Due_Date,Status")] Explore explore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(explore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(explore);
        }

        // GET: ExploreContoller/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Explore == null)
            {
                return NotFound();
            }

            var explore = await _context.Explore.FindAsync(id);
            if (explore == null)
            {
                return NotFound();
            }
            return View(explore);
        }

        // POST: ExploreContoller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Requester,Accepted_By,Due_Date,Status")] Explore explore)
        {
            if (id != explore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(explore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExploreExists(explore.Id))
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
            return View(explore);
        }

        // GET: ExploreContoller/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Explore == null)
            {
                return NotFound();
            }

            var explore = await _context.Explore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (explore == null)
            {
                return NotFound();
            }

            return View(explore);
        }

        // POST: ExploreContoller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Explore == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Explore'  is null.");
            }
            var explore = await _context.Explore.FindAsync(id);
            if (explore != null)
            {
                _context.Explore.Remove(explore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExploreExists(int id)
        {
          return (_context.Explore?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
