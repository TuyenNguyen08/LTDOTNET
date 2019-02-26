using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebBenhVien.EfModels;
using Kendo.Mvc.UI;
using AdminWebBenhVien.Models;
using Kendo.Mvc.Extensions;

namespace AdminWebBenhVien.Controllers
{
    public class LienHeKhachHangsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public LienHeKhachHangsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: LienHeKhachHangs
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.LienHeKhachHang.Include(l => l.FkNguoiXemNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> LienHeKhachHang_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listEvent = _context.LienHeKhachHang.AsNoTracking()
                .Select(t => new LienHeKhachHangViewModel
                {
                    Id = t.Id,
                    HoVaTen = t.HoVaTen,
                    DienThoaiNha=t.DienThoaiNha,
                    DienThoaiDiDong=t.DienThoaiDiDong,
                    Email=t.Email,
                    NoiDung=t.NoiDung.Substring(0,40)+"...",
                    NgayGui=t.NgayGui,
                    DaXem=t.DaXem,
                    FkNguoiXem=t.FkNguoiXem,
                    NgayXem=t.NgayXem,
                    TieuDe=t.TieuDe
                   
                });
            var result = await listEvent.ToDataSourceResultAsync(request);
            return Json(result);
        }
        // GET: LienHeKhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHeKhachHang = await _context.LienHeKhachHang
                .Include(l => l.FkNguoiXemNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lienHeKhachHang == null)
            {
                return NotFound();
            }

            return View(lienHeKhachHang);
        }

        // GET: LienHeKhachHangs/Create
        public IActionResult Create()
        {
            ViewData["FkNguoiXem"] = new SelectList(_context.User, "UserName", "UserName");
            return View();
        }

        // POST: LienHeKhachHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoVaTen,DienThoaiNha,DienThoaiDiDong,Email,NoiDung,NgayGui,DaXem,FkNguoiXem,NgayXem,TieuDe")] LienHeKhachHang lienHeKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lienHeKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNguoiXem"] = new SelectList(_context.User, "UserName", "UserName", lienHeKhachHang.FkNguoiXem);
            return View(lienHeKhachHang);
        }

        // GET: LienHeKhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHeKhachHang = await _context.LienHeKhachHang.FindAsync(id);
            if (lienHeKhachHang == null)
            {
                return NotFound();
            }
            ViewData["FkNguoiXem"] = new SelectList(_context.User, "UserName", "UserName", lienHeKhachHang.FkNguoiXem);
            return View(lienHeKhachHang);
        }

        // POST: LienHeKhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoVaTen,DienThoaiNha,DienThoaiDiDong,Email,NoiDung,NgayGui,DaXem,FkNguoiXem,NgayXem,TieuDe")] LienHeKhachHang lienHeKhachHang)
        {
            if (id != lienHeKhachHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lienHeKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LienHeKhachHangExists(lienHeKhachHang.Id))
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
            ViewData["FkNguoiXem"] = new SelectList(_context.User, "UserName", "UserName", lienHeKhachHang.FkNguoiXem);
            return View(lienHeKhachHang);
        }

        // GET: LienHeKhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHeKhachHang = await _context.LienHeKhachHang
                .Include(l => l.FkNguoiXemNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lienHeKhachHang == null)
            {
                return NotFound();
            }

            return View(lienHeKhachHang);
        }

        // POST: LienHeKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lienHeKhachHang = await _context.LienHeKhachHang.FindAsync(id);
            _context.LienHeKhachHang.Remove(lienHeKhachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LienHeKhachHangExists(int id)
        {
            return _context.LienHeKhachHang.Any(e => e.Id == id);
        }
    }
}
