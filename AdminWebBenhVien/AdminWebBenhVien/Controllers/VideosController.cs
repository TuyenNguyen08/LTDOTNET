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
        [HttpGet]
        [Route("video")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Video_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.Video
                .Include(h => h.FkNgonNguNavigation)
                .OrderBy(h => h.TieuDe)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new VideoIndexViewModel
                {
                   Id=h.Id,

                   TieuDe= h.TieuDe,
                   GioiThieu= h.GioiThieu,

                   DuongDanFile = h.DuongDanFile,
                   Xem = h.LuotXem,

                   NgonNguId = h.FkNgonNgu,
                   NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                   NgayTao = h.NgayTao,
                   NguoiTao = h.NguoiTao,

                   NgaySua = h.NgaySua,
                   NguoiSua= h.UserNguoiSua,
                })
                .ToListAsync();

            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: Videos/Edit/5
        [HttpGet]
        [Route("video/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.Video.AsNoTracking()
                .Include(h => h.FkNgonNguNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new VideoEditViewModel
                {
                    Id = h.Id,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,

                    DuongDanFile = h.DuongDanFile,
                    Xem = h.LuotXem,
                    
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NguoiTao = h.NguoiTao,

                    NgaySua = h.NgaySua,
                    NguoiSua = h.UserNguoiSua,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("video/{id}")]
        public async Task<IActionResult> Edit(VideoEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.Video.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.DuongDanFile = model.DuongDanFile;


            dbItem.NgaySua = DateTime.Now;
            dbItem.UserNguoiSua = "admin";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
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
