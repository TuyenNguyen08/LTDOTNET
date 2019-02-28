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
    public class SlideShowsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public SlideShowsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: SlideShows
        [HttpGet]
        [Route("slideshow-trang-chu")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SlideShows_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.SlideShow
                .Include(h => h.FkNgonNguNavigation)
                .OrderBy(h => h.TieuDe)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new SlideShowIndexViewModel
                {
                    Id = h.Id,

                    TieuDe = h.TieuDe,
                   
                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    LinkSuKien = h.LinkEvent                    
                })
                .ToListAsync();

            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: SlideShows/Edit/5
        [HttpGet]
        [Route("slideshow-trang-chu/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.SlideShow.AsNoTracking()
               .Include(h => h.FkNgonNguNavigation)
               .Where(h => h.Id == id.Value)
               .Select(h => new SlideShowEditViewModel
               {
                   Id = h.Id,

                   TieuDe = h.TieuDe,

                   NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                   LinkSuKien = h.LinkEvent
               })
               .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: SlideShows/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("slideshow-trang-chu/{id}")]
        public async Task<IActionResult> Edit(SlideShowEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.SlideShow.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.TieuDe = model.TieuDe;
            dbItem.LinkEvent = model.LinkSuKien;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }




















        // GET: SlideShows/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShow
                .Include(s => s.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideShow == null)
            {
                return NotFound();
            }

            return View(slideShow);
        }

        // GET: SlideShows/Create
        public async Task<IActionResult> Create()
        {
            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            return View();
        }

        // POST: SlideShows/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,HinhAnh,FkNgonNgu,Stt,IsNewtab,LinkEvent,IsLink")] SlideShow slideShow)
        {
            if (ModelState.IsValid)
            {
                #region Get list master
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                #endregion
                _context.Add(slideShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", slideShow.FkNgonNgu);
            return View(slideShow);
        }

        // GET: SlideShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideShow = await _context.SlideShow
                .Include(s => s.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slideShow == null)
            {
                return NotFound();
            }

            return View(slideShow);
        }

        // POST: SlideShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slideShow = await _context.SlideShow.FindAsync(id);
            _context.SlideShow.Remove(slideShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideShowExists(int id)
        {
            return _context.SlideShow.Any(e => e.Id == id);
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
    }
}
