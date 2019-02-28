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


        [HttpGet]
        [Route("gioi-thieu-khoa-ban/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.KhoaPhong.AsNoTracking()
                .Include(h => h.FkLoaiKhoaPhongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new GioiThieuKhoaBanEditViewModel
                {
                    Id = h.Id,

                    TenLoai = h.FkLoaiKhoaPhongNavigation.TenLoai,

                    TieuDe = h.TieuDeKhoa,
                    GioiThieu = h.GioiThieu,
                    NoiDung = h.NoiDung,
                    Xem = h.LuotXem,
                    
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgaySua = h.NgayCapNhat,
                    NguoiSua = h.UserModify,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: KhoaPhongs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("gioi-thieu-khoa-ban/{id}")]
        public async Task<IActionResult> Edit(GioiThieuKhoaBanEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.KhoaPhong.FirstOrDefaultAsync(h => h.Id == model.Id);

            if(dbItem == null)
            {
                return NotFound();
            }

            dbItem.TieuDeKhoa = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.NoiDung = model.NoiDung;

            dbItem.NgayCapNhat = DateTime.Now;
            dbItem.UserModify = "admin";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
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
