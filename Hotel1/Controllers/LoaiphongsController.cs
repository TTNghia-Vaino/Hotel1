using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel1.Data;
using Hotel1.Models;

namespace Hotel1.Controllers
{
    public class LoaiphongsController : Controller
    {
        private readonly DpContext _context;

        public LoaiphongsController(DpContext context)
        {
            _context = context;
        }

        // GET: Loaiphongs
        public async Task<IActionResult> Index()
        {
            return _context.Loaiphongs != null ?
                        View(await _context.Loaiphongs.ToListAsync()) :
                        Problem("Entity set 'DpContext.Loaiphongs'  is null.");
        }

        // GET: Loaiphongs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Loaiphongs == null)
            {
                return NotFound();
            }

            var loaiphong = await _context.Loaiphongs
                .FirstOrDefaultAsync(m => m.Malp == id);
            if (loaiphong == null)
            {
                return NotFound();
            }

            return View(loaiphong);
        }

        // GET: Loaiphongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loaiphongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Malp,Soluongtoida,Gialoaiphong")] Loaiphong loaiphong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiphong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiphong);
        }

        // GET: Loaiphongs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Loaiphongs == null)
            {
                return NotFound();
            }

            var loaiphong = await _context.Loaiphongs.FindAsync(id);
            if (loaiphong == null)
            {
                return NotFound();
            }
            return View(loaiphong);
        }

        // POST: Loaiphongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Malp,Soluongtoida,Gialoaiphong")] Loaiphong loaiphong)
        {
            if (id != loaiphong.Malp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiphong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiphongExists(loaiphong.Malp))
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
            return View(loaiphong);
        }

        // GET: Loaiphongs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Loaiphongs == null)
            {
                return NotFound();
            }

            var loaiphong = await _context.Loaiphongs
                .FirstOrDefaultAsync(m => m.Malp == id);
            if (loaiphong == null)
            {
                return NotFound();
            }

            return View(loaiphong);
        }

        // POST: Loaiphongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Loaiphongs == null)
            {
                return Problem("Entity set 'DpContext.Loaiphongs'  is null.");
            }
            var loaiphong = await _context.Loaiphongs.FindAsync(id);
            if (loaiphong != null)
            {
                _context.Loaiphongs.Remove(loaiphong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiphongExists(string id)
        {
            return (_context.Loaiphongs?.Any(e => e.Malp == id)).GetValueOrDefault();
        }
    }
}
