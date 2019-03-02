using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebBenhVien.EfModels;
using AdminWebBenhVien.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace AdminWebBenhVien.Controllers
{
    public class DichVuChiTietsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public DichVuChiTietsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: DichVuChiTiets
        [Route("dich-vu")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DichVuChiTiet_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.DichVuChiTiet
                .Include(h => h.FkDichVuNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserModifyNavigation)
                .OrderBy(h => h.FkDichVuNavigation.MoTa)
                .ThenBy(h => h.TenDichVu)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new DichVuIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkDichVu,
                    TenLoai = h.FkDichVuNavigation.MoTa,

                    TieuDe = h.TenDichVu,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgayChinhSua,
                    NguoiSuaId = h.FkUserModify,
                    NguoiSua = h.FkUserModifyNavigation.HoVaTen
                }).ToListAsync();

            var resultJson = await resultDb.ToDataSourceResultAsync(request);
            return Json(resultJson);
        }

        [HttpGet]
        [Route("dich-vu/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.DichVuChiTiet.AsNoTracking()
                .Include(h => h.FkDichVuNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserModifyNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new DichVuEditViewModel
                {
                    Id = h.Id,

                    TenLoai = h.FkDichVuNavigation.MoTa,

                    TieuDe = h.TenDichVu,
                    GioiThieu = h.GioiThieu,
                    NoiDung = h.NoiDung,
                    HinhAnh = h.HinhAnh,
                    Xem = h.LuotXem,
                    
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgayChinhSua,
                    NguoiSua = h.FkUserModifyNavigation.HoVaTen
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("dich-vu/{id}")]
        public async Task<IActionResult> Edit(DichVuEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.DichVuChiTiet.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.TenDichVu = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.NoiDung = model.NoiDung;
            dbItem.HinhAnh = model.HinhAnh;

            dbItem.NgayChinhSua = DateTime.Now;
            dbItem.FkUserModify = "admin";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }






       
        private bool DichVuChiTietExists(int id)
        {
            return _context.DichVuChiTiet.Any(e => e.FkNgonNgu == id);
        }
    }
}
