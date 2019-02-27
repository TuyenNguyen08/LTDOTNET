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
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.SlideShow.Include(s => s.FkNgonNguNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> SlideShows_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listSlideShows = _context.SlideShow.AsNoTracking()
                .Select(t => new SlideShowViewModel
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    HinhAnh=t.HinhAnh,
                    FkNgonNgu = t.FkNgonNgu,
                    Stt=t.Stt,
                    IsNewtab=t.IsNewtab,
                    IsLink=t.IsLink
                });
            var result = await listSlideShows.ToDataSourceResultAsync(request);
            return Json(result);
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

        // GET: SlideShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            var slideShow = await _context.SlideShow.FindAsync(id);
            if (slideShow == null)
            {
                return NotFound();
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", slideShow.FkNgonNgu);
            return View(slideShow);
        }

        // POST: SlideShows/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,HinhAnh,FkNgonNgu,Stt,IsNewtab,LinkEvent,IsLink")] SlideShow slideShow)
        {
            if (id != slideShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    #region Get list master
                    var listNgonNgu = await GetListNgonNguAsync();
                    ViewBag.ListNgonNgu = listNgonNgu;
                    #endregion
                    _context.Update(slideShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideShowExists(slideShow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
