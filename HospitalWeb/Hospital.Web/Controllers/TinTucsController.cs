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

        // GET: TinTucs
        public async Task<IActionResult> Index(int? page)
        {
            var listTinTuc = InitParam.Db.TinTuc.AsNoTracking()
                           .Select(t => new TinTuc
                           {
                               Id = t.Id,
                               TieuDe = t.TieuDe,
                               GioiThieu = t.GioiThieu,
                               HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                               NoiDung = t.NoiDung,
                               NgayTao = t.NgayTao,

                           });


            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfTinTuc = listTinTuc.ToPagedList(pageNumber, 6); // will only contain 25 products max because of the pageSize

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
                .Where(t => t.Id == id)
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
