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
    public class EventsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public EventsController(NBenhVien7CContext context)
        {
            _context = context;
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
            var resultDb = await _context.Event
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
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.Event.AsNoTracking()
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserTaoNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new SuKienEditViewModel
                {
                    Id = h.Id,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Link = h.Link,

                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.Event.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

























        // GET: Events/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tevent = await _context.Event
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
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            ViewData["FkUserTao"] = new SelectList(_context.User, "UserName", "UserName");
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
                _context.Add(tevent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", tevent.FkNgonNgu);
            ViewData["FkUserTao"] = new SelectList(_context.User, "UserName", "UserName", tevent.FkUserTao);
            return View(tevent);
        }

        private Task<List<DropdownlistViewModel>> GetListNgonNguAsync()
        {
            var list = _context.NgonNgu.AsNoTracking()
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

            var tevent = await _context.Event
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
            var tevent = await _context.Event.FindAsync(id);
            _context.Event.Remove(tevent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
