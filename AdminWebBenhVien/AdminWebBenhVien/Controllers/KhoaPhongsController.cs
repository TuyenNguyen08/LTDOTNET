using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebBenhVien.EfModels;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AdminWebBenhVien.Models;

namespace AdminWebBenhVien.Controllers
{
    public class KhoaPhongsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public KhoaPhongsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: KhoaPhongs
        [Route("gioi-thieu-khoa-ban")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> KhoaPhong_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.KhoaPhong
                .Include(h => h.FkLoaiKhoaPhongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .OrderBy(h => h.FkLoaiKhoaPhongNavigation.TenLoai)
                .ThenBy(h => h.TieuDeKhoa)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new GioiThieuKhoaBanIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkLoaiKhoaPhong,
                    TenLoai = h.FkLoaiKhoaPhongNavigation.TenLoai,

                    TieuDe = h.TieuDeKhoa,
                    GioiThieu =  h.GioiThieu,
                    Xem = h.LuotXem,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgaySua = h.NgayCapNhat,
                    NguoiSua = h.UserModify,
                }).ToListAsync();
            var resultJson = await resultDb.ToDataSourceResultAsync(request);
            return Json(resultJson);
        }

        // GET: KhoaPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await _context.KhoaPhong
                .Include(k => k.FkLoaiKhoaPhongNavigation)
                .Include(k => k.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoaPhong == null)
            {
                return NotFound();
            }

            return View(khoaPhong);
        }

        // GET: KhoaPhongs/Create
        public IActionResult Create()
        {
            ViewData["FkLoaiKhoaPhong"] = new SelectList(_context.LoaiKhoaPhong, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            return View();
        }
        // POST: KhoaPhongs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDeKhoa,TenKhoaPhong,GioiThieu,NoiDung,NgayCapNhat,UserModify,FkLoaiKhoaPhong,FkNgonNgu,HinhAnhDaiDien,HenKhamBenh,Stt,LuotXem")] KhoaPhong khoaPhong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoaPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiKhoaPhong"] = new SelectList(_context.LoaiKhoaPhong, "Id", "Id", khoaPhong.FkLoaiKhoaPhong);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", khoaPhong.FkNgonNgu);
            return View(khoaPhong);
        }

        // GET: KhoaPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;

            var listLoaiKhoaPhong = await GetListLoaiKhoaPhongAsync();
            ViewBag.ListLoaiKhoaPhong = listLoaiKhoaPhong;
            var khoaPhong = await _context.KhoaPhong.FindAsync(id);
            if (khoaPhong == null)
            {
                return NotFound();
            }
            ViewData["FkLoaiKhoaPhong"] = new SelectList(_context.LoaiKhoaPhong, "Id", "Id", khoaPhong.FkLoaiKhoaPhong);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", khoaPhong.FkNgonNgu);
            return View(khoaPhong);
        }

        // POST: KhoaPhongs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDeKhoa,TenKhoaPhong,GioiThieu,NoiDung,NgayCapNhat,UserModify,FkLoaiKhoaPhong,FkNgonNgu,HinhAnhDaiDien,HenKhamBenh,Stt,LuotXem")] KhoaPhong khoaPhong)
        {
            if (id != khoaPhong.Id)
            {
                return NotFound();
            }
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;

            var listLoaiKhoaPhong = await GetListLoaiKhoaPhongAsync();
            ViewBag.ListLoaiKhoaPhong = listLoaiKhoaPhong;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaPhongExists(khoaPhong.Id))
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
            ViewData["FkLoaiKhoaPhong"] = new SelectList(_context.LoaiKhoaPhong, "Id", "Id", khoaPhong.FkLoaiKhoaPhong);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", khoaPhong.FkNgonNgu);
            return View(khoaPhong);
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

        private Task<List<DropdownlistViewModel>> GetListLoaiKhoaPhongAsync()
        {
            var list = _context.LoaiKhoaPhong.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenLoai
                }).ToListAsync();

            return list;
        }
        // GET: KhoaPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await _context.KhoaPhong
                .Include(k => k.FkLoaiKhoaPhongNavigation)
                .Include(k => k.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoaPhong == null)
            {
                return NotFound();
            }

            return View(khoaPhong);
        }

        // POST: KhoaPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaPhong = await _context.KhoaPhong.FindAsync(id);
            _context.KhoaPhong.Remove(khoaPhong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaPhongExists(int id)
        {
            return _context.KhoaPhong.Any(e => e.Id == id);
        }
    }
}
