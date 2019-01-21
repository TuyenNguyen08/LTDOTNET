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
    public class HoatDongsController : ControllerBase
    {
        public HoatDongsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: HoatDongs
        public async Task<IActionResult> Index(int? page)
        {
            var listHoatDong = InitParam.Db.HoatDong.AsNoTracking()
                .Include(h => h.FkLoaiHoatDongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkNguoiSuaNavigation)
                .Include(h => h.FkNguoiTaoNavigation)
                .Select( t=> new HoatDong
                {
                    Id=t.Id,
                    TieuDe=t.TieuDe,
                    NgayTao=t.NgayTao,
                    GioiThieu=t.GioiThieu,
                    HinhAnhMinhHoa=t.HinhAnhMinhHoa,
                    NoiDung=t.NoiDung,
                });


            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfTinTuc = listHoatDong.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

            return View(onePageOfTinTuc);
        }

        // GET: HoatDongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDong = await InitParam.Db.HoatDong.AsNoTracking()
                  .Include(t => t.FkNgonNguNavigation)
                  .Include(t =>t.FkLoaiHoatDongNavigation)
                  .Include(t => t.FkNguoiSuaNavigation)
                  .Include(t => t.FkNguoiTaoNavigation)
                  .Where(t => t.Id == id)
                   .Select(t => new HoatDong
                   {
                       Id = t.Id,
                       TieuDe = t.TieuDe,
                       GioiThieu = t.GioiThieu,
                       HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                       NoiDung = t.NoiDung,
                   }).FirstOrDefaultAsync();

            if (hoatDong == null)
            {
                return NotFound();
            }

            return View(hoatDong);
        }

        // GET: HoatDongs/Create
  
    }
}
