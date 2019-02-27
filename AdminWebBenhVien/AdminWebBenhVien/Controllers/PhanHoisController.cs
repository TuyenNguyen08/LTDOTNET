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
    public class PhanHoisController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public PhanHoisController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: PhanHois
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.PhanHoi.Include(p => p.FkNgonNguNavigation).Include(p => p.FkUserTraLoiNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> PhanHoi_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listEvent = _context.PhanHoi.AsNoTracking()
                .Select(t => new PhanHoiViewModel
                {
                    Id = t.Id,
                    NgayKham = t.NgayKham,
                    MaSoBenhNhan = t.MaSoBenhNhan,
                    HoVaTen=t.HoVaTen,
                    Email=t.Email,
                    NgaySinh=t.NgaySinh,
                    GioiTinh=t.GioiTinh,
                    DienThoai=t.DienThoai,
                    NoiDung=t.NoiDung.Substring(0, 40) + "...",
                    NgayGui=t.NgayGui,
                    DaXem=t.DaXem,
                    FkLoaiPhanHoi=t.FkLoaiPhanHoi,
                    FkNgonNgu=t.FkNgonNgu,
                    NoiDungTraLoi=t.NoiDungTraLoi.Substring(0, 40) + "...",
                    NgayTraLoi=t.NgayTraLoi,
                    FkUserTraLoi=t.FkUserTraLoi,
                    DiaChi=t.DiaChi,
                    IsShow=t.IsShow,
                    TieuDe=t.TieuDe
                });
            var result = await listEvent.ToDataSourceResultAsync(request);
            return Json(result);
        }
        // GET: PhanHois/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanHoi = await _context.PhanHoi
                .Include(p => p.FkNgonNguNavigation)
                .Include(p => p.FkUserTraLoiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            return View(phanHoi);
        }

        // GET: PhanHois/Create
        public async Task<IActionResult> Create()
        {
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName");
            return View();
        }

        // POST: PhanHois/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgayKham,MaSoBenhNhan,HoVaTen,Email,NgaySinh,GioiTinh,DienThoai,NoiDung,NgayGui,DaXem,FkLoaiPhanHoi,FkNgonNgu,NoiDungTraLoi,NgayTraLoi,FkUserTraLoi,DiaChi,IsShow,TieuDe")] PhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                _context.Add(phanHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", phanHoi.FkNgonNgu);
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName", phanHoi.FkUserTraLoi);
            return View(phanHoi);
        }

        // GET: PhanHois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            var phanHoi = await _context.PhanHoi.FindAsync(id);
            if (phanHoi == null)
            {
                return NotFound();
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", phanHoi.FkNgonNgu);
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName", phanHoi.FkUserTraLoi);
            return View(phanHoi);
        }

        // POST: PhanHois/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayKham,MaSoBenhNhan,HoVaTen,Email,NgaySinh,GioiTinh,DienThoai,NoiDung,NgayGui,DaXem,FkLoaiPhanHoi,FkNgonNgu,NoiDungTraLoi,NgayTraLoi,FkUserTraLoi,DiaChi,IsShow,TieuDe")] PhanHoi phanHoi)
        {
            if (id != phanHoi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var listNgonNgu = await GetListNgonNguAsync();
                    ViewBag.ListNgonNgu = listNgonNgu;
                    _context.Update(phanHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhanHoiExists(phanHoi.Id))
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
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", phanHoi.FkNgonNgu);
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName", phanHoi.FkUserTraLoi);
            return View(phanHoi);
        }

        // GET: PhanHois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanHoi = await _context.PhanHoi
                .Include(p => p.FkNgonNguNavigation)
                .Include(p => p.FkUserTraLoiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            return View(phanHoi);
        }

        // POST: PhanHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phanHoi = await _context.PhanHoi.FindAsync(id);
            _context.PhanHoi.Remove(phanHoi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhanHoiExists(int id)
        {
            return _context.PhanHoi.Any(e => e.Id == id);
        }

        private Task<List<DropdownlistViewModel>> GetListLoaiPhanHoiAsync()
        {
            var list = _context.TrangThai.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenLoaiTrangThai
                }).ToListAsync();

            return list;
        }
        private Task<List<DropdownlistViewModel>> GetListNgonNguAsync()
        {
            var list = _context.NgonNgu.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenNgonNgu
                }).ToListAsync();

            return list;
        }
    }
}
