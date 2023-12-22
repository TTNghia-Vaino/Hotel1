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
    public class PhongsController : Controller
    {
        private readonly DpContext _context;

        public PhongsController(DpContext context)
        {
            _context = context;
        }

        // GET: Phongs
        public async Task<IActionResult> Index(string searchString)
        {
            var phongs = _context.Phongs
                .Include(p => p.LoaiphongNavigation)
                .OrderBy(p => p.Maphong);

            if (!string.IsNullOrEmpty(searchString))
            {
                phongs = (IOrderedQueryable<Phong>)phongs.Where(p =>
                    p.Maphong.Contains(searchString) ||
                    p.LoaiphongNavigation.Malp.Contains(searchString)
                );
            }

            return View(await phongs.ToListAsync());
        }



        // GET: Phongs/Create
        public IActionResult Create()
        {
            ViewData["Loaiphong"] = new SelectList(_context.Loaiphongs, "Malp", "Malp");
            return View();
        }

        // POST: Phongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maphong,Loaiphong,Trangthai")] Phong phong)
        {
            var loaiphong = _context.Loaiphongs.FirstOrDefault(p => p.Malp == phong.Loaiphong);

            if (loaiphong != null)
            {
                var phongCount = _context.Phongs.Count(p => p.Loaiphong == phong.Loaiphong);

                if (phongCount < loaiphong.Soluongtoida)
                {
                    _context.Add(phong);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The maximum quantity for this Loaiphong has been reached.");
                    return View(phong); // Returning the view with the model to show error messages.
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Loaiphong.");
                return View(phong); // Returning the view with the model to show error messages.
            }
        }



        private List<SelectListItem> GetLoaiphongItems()
        {
            // Retrieve and create a list of Loaiphong options
            var loaiphongItems = _context.Loaiphongs
                .Select(lp => new SelectListItem
                {
                    Value = lp.Malp,
                    Text = lp.Malp // Replace with the appropriate property to display in the dropdown
                })
                .ToList();

            return loaiphongItems;
        }
        // GET: Phongs/Edit/5


        // POST: Phongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        private List<SelectListItem> GetTrangthaiItems()
        {
            // Define your Trangthai options here
            var trangthaiItems = new List<SelectListItem>
    {
        new SelectListItem { Value = "DatTruoc", Text = "Đặt trước" },
        new SelectListItem { Value = "SanSang", Text = "Sẵn sàng" },
        new SelectListItem { Value = "CoKhach", Text = "Có khách" },
        new SelectListItem { Value = "BaoTri", Text = "Bảo trì" },
        new SelectListItem { Value = "DonDep", Text = "Dọn dẹp" }
        // Add other options as needed
    };

            return trangthaiItems;
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null)
            {
                return NotFound();
            }

            ViewData["Loaiphong"] = new SelectList(_context.Loaiphongs, "Malp", "Tenlp", phong.Loaiphong);
            ViewBag.LoaiphongItems = GetLoaiphongItems();
            ViewBag.TrangthaiItems = GetTrangthaiItems();

            return View(phong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Maphong,Loaiphong,Trangthai")] Phong phong)
        {
            var existingPhong = await _context.Phongs.FindAsync(phong.Maphong);
            var existingLoaiPhong = await _context.Loaiphongs.FirstOrDefaultAsync(lp => lp.Malp == phong.Loaiphong);

            if (existingPhong == null || existingLoaiPhong == null)
            {
                return NotFound();
            }

            var phongCount = await _context.Phongs.CountAsync(p => p.Loaiphong == phong.Loaiphong);

            if (phongCount < existingLoaiPhong.Soluongtoida)
            {
                existingPhong.Loaiphong = phong.Loaiphong;
                existingPhong.Trangthai = phong.Trangthai;

                try
                {
                    _context.Update(existingPhong);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Error updating the Phong.");
                    // Handle the exception or log the error
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Số phòng đã đạt tối đa");
            }

            ViewBag.LoaiphongItems = GetLoaiphongItems();
            ViewBag.TrangthaiItems = GetTrangthaiItems();

            return View(phong);
        }








        // GET: Phongs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Phongs == null)
            {
                return NotFound();
            }

            var phong = await _context.Phongs
                .Include(p => p.LoaiphongNavigation)
                .FirstOrDefaultAsync(m => m.Maphong == id);
            if (phong == null)
            {
                return NotFound();
            }

            return View(phong);
        }

        // POST: Phongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Phongs == null)
            {
                return Problem("Entity set 'DpContext.Phongs'  is null.");
            }
            var phong = await _context.Phongs.FindAsync(id);
            if (phong != null)
            {
                _context.Phongs.Remove(phong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongExists(string id)
        {
            return (_context.Phongs?.Any(e => e.Maphong == id)).GetValueOrDefault();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyMaphong(string Maphong)
        {
            var existingPhong = _context.Phongs.FirstOrDefault(p => p.Maphong == Maphong);

            if (existingPhong != null)
            {
                return Json($"Maphong {Maphong} already exists.");
            }

            return Json(true);
        }
    }
}
