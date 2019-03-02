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
    public class DichVusController : ControllerBase
    {

        public DichVusController(InitParam initParam) : base(initParam)
        {
        }

        // GET: DichVus
        public async Task<IActionResult> Index()
        {
            var fkNgonNgu = base.NgonNgu;
            var lstDichVuChiTiet = await InitParam.Db.DichVuChiTiet.AsNoTracking()
                   .Where(h => h.FkNgonNgu == fkNgonNgu)
                .Select(t => new DichVuChiTiet
                {
                   Id = t.Id,
                   TenDichVu=t.TenDichVu,
                }).ToListAsync();

            return View(lstDichVuChiTiet);
        }
        // GET: DichVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var fkNgonNgu = base.NgonNgu;
            if (id == null)
            {
                return NotFound();
            }

            #region lsDichVuChiTiet
            var lstdichVuChiTiet = await InitParam.Db.DichVuChiTiet.AsNoTracking()                
                .Where(t => t.Id == id && t.FkNgonNgu == fkNgonNgu  )
                .Select(t => new DichVuChiTiet
                {
                    Id = t.Id,
                    TenDichVu = t.TenDichVu,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                    NgayTao = t.NgayTao,
                    LuotXem = t.LuotXem,
                }).FirstOrDefaultAsync();

            if (lstdichVuChiTiet == null)
            {
                return NotFound();
            }

            var lsDichVuDetail = await InitParam.Db.DichVuChiTiet.AsNoTracking()
               .Where( t => t.FkNgonNgu == fkNgonNgu )
               .Take(10)
               .Select(t => new DichVuChiTiet
               {
                   Id = t.Id,
                   TenDichVu = t.TenDichVu,

               })
               .OrderByDescending( t => t.NgayTao)
               .ThenByDescending( t => t.NgayChinhSua)
               
               .ToListAsync();

            #endregion

            lsDichVuDetail.RemoveAll(c => c.Id == id);

            ViewBag.lsDichVuCT = lsDichVuDetail;

            return View(lstdichVuChiTiet);
        }

        // GET: DichVus/Create
  
    }
}
