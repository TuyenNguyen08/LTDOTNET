using AdminWebBenhVien.EfModels;
using AdminWebBenhVien.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebBenhVien.Controllers
{
    public class GioiThieuChiTietsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public GioiThieuChiTietsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("gioi-thieu-tong-quan")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GioiThieuChiTiet_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.GioiThieuChiTiet
                .Include(h => h.FkGioiThieuNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserModifyNavigation)
                .OrderBy(h => h.FkGioiThieuNavigation.TenGioiThieu)
                .ThenBy(h => h.Ten)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new GioiThieuTongQuanIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkGioiThieu,
                    TenLoai = h.FkGioiThieuNavigation.TenGioiThieu,

                    TieuDe = h.Ten,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgayChinhSua,
                    NguoiSuaId = h.FkUserModify,
                    NguoiSua = h.FkUserModifyNavigation.HoVaTen
                })
                .ToListAsync();

            var resultJson = await resultDb.ToDataSourceResultAsync(request);
            return Json(resultJson);
        }



        [HttpGet]
        [Route("gioi-thieu-tong-quan/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.GioiThieuChiTiet.AsNoTracking()
                .Include(h => h.FkGioiThieuNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserModifyNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new GioiThieuTongQuanEditViewModel
                {
                    Id = h.Id,

                    TenLoai = h.FkGioiThieuNavigation.TenGioiThieu,

                    TieuDe = h.Ten,
                    GioiThieu = h.GioiThieu,
                    NoiDung = h.NoiDung,
                    Xem = h.LuotXem,
                    HinhAnh = h.HinhAnh,

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
        [Route("gioi-thieu-tong-quan/{id}")]
        public async Task<IActionResult> Edit(GioiThieuTongQuanEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.GioiThieuChiTiet.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.Ten = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.NoiDung = model.NoiDung;
            dbItem.HinhAnh = model.HinhAnh;

            dbItem.NgayChinhSua = DateTime.Now;
            dbItem.FkUserModify = "admin";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
    }
}
