using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebBenhVien.EfModels;
using Kendo.Mvc.UI;
using AdminWebBenhVien.Models;
using Kendo.Mvc.Extensions;

namespace AdminWebBenhVien.Controllers
{
    public class EventsController : ControllerBase
    {
       

        public EventsController(InitParam initParam) : base(initParam)
        {
           
        }

        // GET: Events
        [HttpGet]
        [Route("su-kien-quang-cao")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Event_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await InitParam.Db.Event
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserTaoNavigation)
                .OrderBy(h => h.TieuDe)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new SuKienIndexViewModel
                {
                    Id = h.Id,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Link = h.Link,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NguoiTaoId = h.FkUserTao,
                    NguoiTao = h.FkUserTaoNavigation.HoVaTen,

                }).ToListAsync();

            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: Events/Edit/5
        [HttpGet]
        [Route("su-kien-quang-cao/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;

            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await InitParam.Db.Event.AsNoTracking()
                .Include(h => h.FkUserTaoNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new SuKienEditViewModel
                {
                    Id = h.Id,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    HinhAnh = h.HinhAnh,
                    Link = h.Link,

                    NgonNguId = h.FkNgonNgu,

                    NgayTao = h.NgayTao,
                    NguoiTao = h.FkUserTaoNavigation.HoVaTen,

                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);

        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("su-kien-quang-cao/{id}")]
        public async Task<IActionResult> Edit(SuKienEditViewModel model)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await InitParam.Db.Event.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.FkNgonNgu = model.NgonNguId;
            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.HinhAnh = model.HinhAnh;
            dbItem.Link = model.Link;

            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        [HttpGet]
        [Route("tao-moi-su-kien")]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;

            return View(new SuKienCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi-su-kien")]
        public async Task<IActionResult> Create(SuKienCreateViewModel model)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = new Event();

            dbItem.FkNgonNgu = model.NgonNguId;
            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.HinhAnh = model.HinhAnh;
            dbItem.Link = model.Link;

            dbItem.NgayTao = DateTime.Now;
            dbItem.FkUserTao = "admin";

            InitParam.Db.Event.Add(dbItem);
            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = dbItem.Id });
        }





















        // GET: Events/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tevent = await InitParam.Db.Event
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tevent == null)
            {
                return NotFound();
            }

            return View(tevent);
        }

        // GET: Events/Create
        public async Task<IActionResult> Create()
        {
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
            ViewData["FkUserTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,GioiThieu,HinhAnh,Link,FkNgonNgu,NgayTao,FkUserTao,Stt,NewTab")] Event tevent)
        {
            if (ModelState.IsValid)
            {
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                InitParam.Db.Add(tevent);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", tevent.FkNgonNgu);
            ViewData["FkUserTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tevent.FkUserTao);
            return View(tevent);
        }

        private Task<List<DropdownlistViewModel>> GetListNgonNguAsync()
        {
            var list = InitParam.Db.NgonNgu.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenNgonNgu
                }).ToListAsync();

            return list;
        }
        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tevent = await InitParam.Db.Event
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tevent == null)
            {
                return NotFound();
            }

            return View(tevent);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tevent = await InitParam.Db.Event.FindAsync(id);
            InitParam.Db.Event.Remove(tevent);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return InitParam.Db.Event.Any(e => e.Id == id);
        }
    }
}
