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
    public class EventsController : ControllerBase
    {


        public EventsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: Events
        public async Task<IActionResult> Index(int? page)
        {
            var listEvent = InitParam.Db.Event.AsNoTracking()
                .Where(t => t.FkNgonNgu == NgonNgu)
                .Select(t =>new Event {
                    Id=t.Id,
                    TieuDe=t.TieuDe,
                    GioiThieu=t.GioiThieu,
                    HinhAnh=t.HinhAnh,
                    Link=t.Link,
                    NgayTao=t.NgayTao,
                    NewTab=t.NewTab,

 
            });
            var pageNumber = page ?? 1;
            var onePageOfTinTuc = listEvent.ToPagedList(pageNumber, 6);

            return View(onePageOfTinTuc);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sukien = await InitParam.Db.Event.AsNoTracking()
               .Where(t => t.Id == id && t.FkNgonNgu == NgonNgu)
               .Select(t => new Event
               {
                   Id=t.Id,
                   TieuDe = t.TieuDe,
                   GioiThieu = t.GioiThieu,
                  
               }).FirstOrDefaultAsync();
            if (sukien == null)
            {
                return NotFound();
            }

            return View(sukien);
        }

        // GET: Events/Create
  
    }
}
