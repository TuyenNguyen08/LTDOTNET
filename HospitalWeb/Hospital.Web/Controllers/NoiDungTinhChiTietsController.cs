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
    public class NoiDungTinhChiTietsController : ControllerBase
    {
        public NoiDungTinhChiTietsController(InitParam initParam) : base(initParam)
        {
        }

        // GET: NoiDungTinhChiTiets
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = InitParam.Db.NoiDungTinhChiTiet
                .Include(n => n.FkNgonNguNavigation)
                .Include(n => n.FkNoiDungTinhNavigation)
                .Include(n => n.FkUserChinhsuaNavigation);

            return View(await nBenhVien7CContext.ToListAsync());
        }

        // GET: NoiDungTinhChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var fkNgonNgu = base.NgonNgu;
            if (id == null)
            {
                return NotFound();
            }

            #region lsThongTinKhac
            var noiDungTinhChiTiet = await InitParam.Db.NoiDungTinhChiTiet.AsNoTracking()
                .Where(t => t.Id == id && t.FkNgonNgu == fkNgonNgu)
                .Select(t => new NoiDungTinhChiTiet
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                }).FirstOrDefaultAsync();
            if (noiDungTinhChiTiet == null)
            {
                return NotFound();
            }
            #region listNoiDungTinh

            var listNoiDungTinh = await InitParam.Db.NoiDungTinh.AsNoTracking().Take(9).Select(t => new NoiDungTinh
            {
                Id = t.Id,
                TenNoiDung=t.TenNoiDung

            }).ToListAsync();

            ViewBag.lsNoiDungTinh = listNoiDungTinh;

            #endregion

            var lsDungTinhChiTiet = await InitParam.Db.NoiDungTinhChiTiet.AsNoTracking()
               .Where(t => t.FkNgonNgu == fkNgonNgu)
               .Take(10)
               .Select(t => new NoiDungTinhChiTiet
               {
                   Id = t.Id,
                   TieuDe = t.TieuDe,
               })
               .OrderByDescending(t => t.NgayChinhSua)
               .ToListAsync();

            lsDungTinhChiTiet.RemoveAll(c => c.Id == id);

            ViewBag.lsNDChiTiet = lsDungTinhChiTiet;

            #endregion
            return View(noiDungTinhChiTiet);
        }
    }
}
