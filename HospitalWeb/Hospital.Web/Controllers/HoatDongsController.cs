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

        // Activities.aspx? type = 1
        public async Task<IActionResult> Index(int? id, int? page)
        {

            var query = InitParam.Db.HoatDong.AsNoTracking()
                .Where(h => h.FkNgonNgu == NgonNgu);
            if (id.HasValue && id.Value > 0)
            {
                query = query.Where(h => h.FkLoaiHoatDong == id);
            }

            query = query.OrderByDescending(h => h.NgayTao);

            var listResult = query.Select( t=> new HoatDong
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                    NgayTao = t.NgayTao,
                });


            var pageNumber = page ?? 1;
            var onePageOfTinTuc = await listResult.ToPagedListAsync(pageNumber, 6);

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
                  .Where(t => t.Id == id && t.FkNgonNgu == NgonNgu)
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
