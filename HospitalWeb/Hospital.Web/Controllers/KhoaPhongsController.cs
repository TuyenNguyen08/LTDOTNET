using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;

namespace Hospital.Web.Controllers
{
    public class KhoaPhongsController : ControllerBase
    {

        public KhoaPhongsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: KhoaPhongs
        public async Task<IActionResult> Index()
        {
            var listKhoaPhong = await InitParam.Db.KhoaPhong.AsNoTracking()
                .Select(t => new KhoaPhong
                {
                    Id = t.Id,
                    TenKhoaPhong = t.TenKhoaPhong,
                    HinhAnhDaiDien=t.HinhAnhDaiDien,
                }).ToListAsync();

            return View(listKhoaPhong);
        }

        // GET: KhoaPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await InitParam.Db.KhoaPhong.AsNoTracking()
                .Include(k => k.FkLoaiKhoaPhongNavigation)
                .Include(k => k.FkNgonNguNavigation)
                .Where(m => m.Id == id)
                .Select(t => new KhoaPhong
                {
                    Id = t.Id,
                    TieuDeKhoa=t.TieuDeKhoa,
                    TenKhoaPhong = t.TenKhoaPhong,
                    GioiThieu=t.GioiThieu,
                    NoiDung=t.NoiDung,
                    FkLoaiKhoaPhongNavigation = t.FkLoaiKhoaPhongNavigation
                }).FirstOrDefaultAsync();
            ;

            if (khoaPhong == null)
            {
                return NotFound();
            }

            return View(khoaPhong);
        }


    }
}
