using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanTemplate2023.Models;

namespace KanbanTemplate2023.Controllers
{
    public class KanBanTasksController : Controller
    {
        private readonly KanBanDbContext _context;

        public KanBanTasksController(KanBanDbContext context)
        {
            _context = context;
        }

        // GET: KanBanTasks
        public async Task<IActionResult> Index()
        {
              return View(await _context.Tasks.ToListAsync());
        }

        // GET: KanBanTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null ) //|| _context.Tasks == null)
            {
                return NotFound();
            }

            var kanBanTask = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kanBanTask == null)
            {
                return NotFound();
            }

            return View(kanBanTask);
        }

        // GET: KanBanTasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KanBanTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] KanBanTask kanBanTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kanBanTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kanBanTask);
        }

        // GET: KanBanTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var kanBanTask = await _context.Tasks.FindAsync(id);
            if (kanBanTask == null)
            {
                return NotFound();
            }
            return View(kanBanTask);
        }

        // POST: KanBanTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] KanBanTask kanBanTask)
        {
            if (id != kanBanTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kanBanTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KanBanTaskExists(kanBanTask.Id))
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
            return View(kanBanTask);
        }

        // GET: KanBanTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var kanBanTask = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kanBanTask == null)
            {
                return NotFound();
            }

            return View(kanBanTask);
        }

        // POST: KanBanTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'KanBanDbContext.Tasks'  is null.");
            }
            var kanBanTask = await _context.Tasks.FindAsync(id);
            if (kanBanTask != null)
            {
                _context.Tasks.Remove(kanBanTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KanBanTaskExists(int id)
        {
          return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
