using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;
using X.PagedList;

namespace Hospital.Web.Controllers
{
    public class HoiDapYkhoasController : ControllerBase
    {
        public HoiDapYkhoasController(InitParam initParam) : base(initParam)
        {

        }

        // GET: HoiDapYkhoas
        public async Task<IActionResult> Index(int? page)
        {
            var fkNgonNgu = base.NgonNgu;
            var listHoiDap = InitParam.Db.HoiDapYkhoa.AsNoTracking()
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkUserTraLoiNavigation)
                .Select(t => new HoiDapYkhoa
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    NoiDungCauHoi = t.NoiDungCauHoi,
                    HoTenNguoiHoi = t.HoTenNguoiHoi,
                    GioiTinh = t.GioiTinh,
                    NoiDungTraLoi = t.NoiDungTraLoi,
                    NgayGuiCauHoi = t.NgayGuiCauHoi,
                    NgayTraLoi = t.NgayTraLoi,
                    NguoiTraLoi = t.NguoiTraLoi,
                });
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfHoiDap = listHoiDap.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

            return View(onePageOfHoiDap);
        }

       
        // GET: HoiDapYkhoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var fkNgonNgu = base.NgonNgu;
            if (id == null)
            {
                return NotFound();
            }
          

            var hoiDapYkhoa = await InitParam.Db.HoiDapYkhoa
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkUserTraLoiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoiDapYkhoa == null)
            {
                return NotFound();
            }

            #region listHoiDap

            var lsHoiDap = await InitParam.Db.HoiDapYkhoa.AsNoTracking()
                .Take(10)
             .Select(t => new HoiDapYkhoa
             {
                 Id = t.Id,
                 TieuDe = t.TieuDe,

             })
             .OrderByDescending(t => t.NgayTraLoi)
             .ThenByDescending(t => t.NgayGuiCauHoi)
             .ToListAsync();

            #endregion

            lsHoiDap.RemoveAll(c => c.Id == id);

            ViewBag.listsHoiDap = lsHoiDap;

            return View(hoiDapYkhoa);
        }
    }
}
