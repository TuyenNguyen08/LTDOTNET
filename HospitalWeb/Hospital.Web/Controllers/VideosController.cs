using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Hospital.Web.Controllers
{
    public class VideosController : ControllerBase
    {

        public VideosController(InitParam initParam) : base(initParam)
        {

        }

        public async Task<IActionResult> Index(int? page)
        {
            var listVideo = InitParam.Db.Video.AsNoTracking()
                .Select(t => new Video
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    HinhAnh = t.HinhAnh,
                    DuongDanFile = t.DuongDanFile,
                  
                });
            var pageNumber = page ?? 1;
            var onePageOfVideo = listVideo.ToPagedList(pageNumber, 6);

            return View(onePageOfVideo);
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sukien = await InitParam.Db.Video.AsNoTracking()
               .Where(t => t.Id == id)
               .Select(t => new Video
               {
                   Id = t.Id,
                   TieuDe = t.TieuDe,
                   GioiThieu = t.GioiThieu,
                   LuotXem = t.LuotXem,
                   NgayTao = t.NgayTao

               }).FirstOrDefaultAsync();
            if (sukien == null)
            {
                return NotFound();
            }

            return View(sukien);
        }
    }
}