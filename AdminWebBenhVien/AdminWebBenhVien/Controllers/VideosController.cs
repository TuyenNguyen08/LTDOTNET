using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebBenhVien.EfModels;
using AdminWebBenhVien.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace AdminWebBenhVien.Controllers
{
    public class VideosController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public VideosController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.Video.Include(v => v.FkNgonNguNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Video_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listEvent = _context.Video.AsNoTracking()
                .Select(t => new VideoViewModel
                {
                   Id=t.Id,
                   TieuDe=t.TieuDe,
                   GioiThieu=t.GioiThieu,
                   DuongDanFile=t.DuongDanFile,
                   IsYoutube=t.IsYoutube,
                   NoiBat=t.NoiBat,
                   LuotXem=t.LuotXem,
                   FkNgonNgu=t.FkNgonNgu,
                   NgayTao=t.NgayTao,
                   UserNguoiSua=t.UserNguoiSua,
                   NgaySua=t.NgaySua,
                   Stt=t.Stt,
                   HinhAnh=t.HinhAnh
                });
            var result = await listEvent.ToDataSourceResultAsync(request);
            return Json(result);
        }
        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Video
                .Include(v => v.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public async Task<IActionResult> Create()
        {
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,GioiThieu,DuongDanFile,IsYoutube,NoiBat,LuotXem,FkNgonNgu,NgayTao,NguoiTao,UserNguoiSua,NgaySua,Stt,HinhAnh")] Video video)
        {
            if (ModelState.IsValid)
            {
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", video.FkNgonNgu);
            return View(video);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            var video = await _context.Video.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", video.FkNgonNgu);
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,GioiThieu,DuongDanFile,IsYoutube,NoiBat,LuotXem,FkNgonNgu,NgayTao,NguoiTao,UserNguoiSua,NgaySua,Stt,HinhAnh")] Video video)
        {
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var listNgonNgu = await GetListNgonNguAsync();
                    ViewBag.ListNgonNgu = listNgonNgu;
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
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
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", video.FkNgonNgu);
            return View(video);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Video
                .Include(v => v.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Video.FindAsync(id);
            _context.Video.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Video.Any(e => e.Id == id);
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
