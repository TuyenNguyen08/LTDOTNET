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
    public class TinTucsController : ControllerBase
    {
        public TinTucsController(InitParam initParam) : base(initParam)
        {

        }

        // Thread.aspx?type=1
        public async Task<IActionResult> Index(int? id, int? page)
        {

            var query = InitParam.Db.TinTuc.AsNoTracking()
                .Where(h => h.FkNgonNgu == NgonNgu);
            if (id.HasValue && id.Value > 0)
            {
                query = query.Where(h => h.FkLoaiTin == id);
            }

            query = query.OrderByDescending(h => h.NgayTao);


            var listResult = query.Select(t => new TinTuc
            {
                Id = t.Id,
                TieuDe = t.TieuDe,
                GioiThieu = t.GioiThieu,
                HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                NgayTao = t.NgayTao,
            });


            var pageNumber = page ?? 1;
            var onePageOfTinTuc = await listResult.ToPagedListAsync(pageNumber, PageSize);

            return View(onePageOfTinTuc);
        }

        // GET: TinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await InitParam.Db.TinTuc.AsNoTracking()
                .Include(t => t.FkLoaiTinNavigation)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserNguoiSuaNavigation)
                .Include(t => t.FkUserNguoiTaoNavigation)
                .Select(t => new TinTuc
                {
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                    NgayTao = t.NgayTao,
                    NoiDung = t.NoiDung,
                    LuotXem = t.LuotXem,
                    Author = t.Author,
                }).FirstOrDefaultAsync();
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }
    }
}
