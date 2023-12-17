using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DongHoShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DongHoShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class LoaiSanPhamController : Controller
    {
        private readonly DongHoShopContext _context;

        public LoaiSanPhamController(DongHoShopContext context)
        {
            _context = context;
        }

        // GET: LoaiSanPham
        public async Task<IActionResult> Index()
        {
            return _context.LoaiSanPham != null ?
                        View(await _context.LoaiSanPham.ToListAsync()) :
                        Problem("Entity set 'DongHoShopContext.LoaiSanPham'  is null.");
        }

        // GET: LoaiSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiSanPham == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPham
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // GET: LoaiSanPham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenLoai")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSanPham);
        }

        // GET: LoaiSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiSanPham == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPham.FindAsync(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View(loaiSanPham);
        }

        // POST: LoaiSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenLoai")] LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSanPhamExists(loaiSanPham.ID))
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
            return View(loaiSanPham);
        }

        // GET: LoaiSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiSanPham == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPham
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // POST: LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiSanPham == null)
            {
                return Problem("Entity set 'DongHoShopContext.LoaiSanPham'  is null.");
            }
            var loaiSanPham = await _context.LoaiSanPham.FindAsync(id);
            if (loaiSanPham != null)
            {
                _context.LoaiSanPham.Remove(loaiSanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSanPhamExists(int id)
        {
            return (_context.LoaiSanPham?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
