using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanTemplate2023.Models;
using KanbanTemplate2023.Services;

namespace KanbanTemplate2023.Controllers
{
    public class PeopleController : Controller
    {
        //  private readonly KanBanDbContext _context;
        private readonly IPersonService _service;

        public PeopleController(IPersonService service)
        {
            _service = service;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
              return View(await _service.GetAllAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null  ||  _service.IsAvailable())
            {
                return NotFound();
            }

            var person = await _service.GetByIdAsync(id)  ;
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Name")] Person person)
        {
            if (ModelState.IsValid)
            {
                await _service.Save(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null ) //|| _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _service.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,Name")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {await _service.Update(person);
                
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _service.IsAvailable())
            {
                return NotFound();
            }

            var person = await _service.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_service.IsAvailable())
            {
                return Problem("Entity set 'KanBanDbContext.Persons'  is null.");
            }
            var  person = await _service.GetByIdAsync(id);
            await _service.Delete(person);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
