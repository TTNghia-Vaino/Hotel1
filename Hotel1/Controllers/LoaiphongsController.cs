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
                var existingLoaiphong = await _context.Loaiphongs.FindAsync(id);
                if (existingLoaiphong == null)
                {
                    return NotFound();
                }

                try
                {
                    // Modify the properties of the existing entity
                    existingLoaiphong.Malp = loaiphong.Malp; // If you wish to change Malp
                    existingLoaiphong.Soluongtoida = loaiphong.Soluongtoida;
                    existingLoaiphong.Gialoaiphong = loaiphong.Gialoaiphong;

                    _context.Update(existingLoaiphong);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Error updating Loaiphong.");
                    // Log the error or handle the exception
                }
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

        
        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyMalp(string Malp)
        {
            // Thực hiện kiểm tra giá trị Malp trong cơ sở dữ liệu
            // Trả về Json true nếu không trùng, ngược lại trả về Json thông báo lỗi
            var isMalpValid = _context.Loaiphongs.FirstOrDefault(p => p.Malp == Malp);
            if (isMalpValid!= null)
            {

                return Json("Loại phòng đã tồn tại");
            }
            return Json(true);
        }

    }
}
