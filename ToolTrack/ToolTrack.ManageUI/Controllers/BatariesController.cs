using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core;

namespace ToolTrack.ManageUI.Controllers
{
    public class BatariesController : Controller
    {
        private readonly TTContext _context;

        public BatariesController(TTContext context)
        {
            _context = context;
        }

        // GET: Bataries
        public async Task<IActionResult> Index()
        {
            var tTContext = _context.Bataries.Include(b => b.BataryModel).Include(b => b.Condition).Include(b => b.LastWorker);
            return View(await tTContext.ToListAsync());
        }

        // GET: Bataries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batary = await _context.Bataries
                .Include(b => b.BataryModel)
                .Include(b => b.Condition)
                .Include(b => b.LastWorker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (batary == null)
            {
                return NotFound();
            }

            return View(batary);
        }

        // GET: Bataries/Create
        public IActionResult Create()
        {
            ViewData["BataryModelId"] = new SelectList(_context.BataryModels, "Id", "Name");
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name");
            ViewData["LastWorkerId"] = new SelectList(_context.Workers, "Id", "Email");
            return View();
        }

        // POST: Bataries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BataryModelId,DateMade,SerialNumber,Number,Price,ConditionId,LastWorkerId,LastLocationId")] Batary batary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(batary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BataryModelId"] = new SelectList(_context.BataryModels, "Id", "Name", batary.BataryModelId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name", batary.ConditionId);
            ViewData["LastWorkerId"] = new SelectList(_context.Workers, "Id", "Email", batary.LastWorkerId);
            return View(batary);
        }

        // GET: Bataries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batary = await _context.Bataries.FindAsync(id);
            if (batary == null)
            {
                return NotFound();
            }
            ViewData["BataryModelId"] = new SelectList(_context.BataryModels, "Id", "Name", batary.BataryModelId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name", batary.ConditionId);
            ViewData["LastWorkerId"] = new SelectList(_context.Workers, "Id", "Email", batary.LastWorkerId);
            return View(batary);
        }

        // POST: Bataries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BataryModelId,DateMade,SerialNumber,Number,Price,ConditionId,LastWorkerId,LastLocationId")] Batary batary)
        {
            if (id != batary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BataryExists(batary.Id))
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
            ViewData["BataryModelId"] = new SelectList(_context.BataryModels, "Id", "Name", batary.BataryModelId);
            ViewData["ConditionId"] = new SelectList(_context.Conditions, "Id", "Name", batary.ConditionId);
            ViewData["LastWorkerId"] = new SelectList(_context.Workers, "Id", "Email", batary.LastWorkerId);
            return View(batary);
        }

        // GET: Bataries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batary = await _context.Bataries
                .Include(b => b.BataryModel)
                .Include(b => b.Condition)
                .Include(b => b.LastWorker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (batary == null)
            {
                return NotFound();
            }

            return View(batary);
        }

        // POST: Bataries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batary = await _context.Bataries.FindAsync(id);
            if (batary != null)
            {
                _context.Bataries.Remove(batary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BataryExists(int id)
        {
            return _context.Bataries.Any(e => e.Id == id);
        }
    }
}
