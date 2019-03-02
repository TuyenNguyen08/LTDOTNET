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
    public class VideosController : ControllerBase
    {     
        public VideosController(InitParam initParam) : base(initParam)
        {
        }

        [HttpGet]
        [Route("video")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Video_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await InitParam.Db.Video
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

        [HttpGet]
        [Route("video/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;

            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await InitParam.Db.Video.AsNoTracking()
                .Include(h => h.FkNgonNguNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new VideoEditViewModel
                {
                    Id = h.Id,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,

                    DuongDanFile = h.DuongDanFile,
                    Xem = h.LuotXem,
                    HinhAnh = h.HinhAnh,
                    
                    NgonNguId = h.FkNgonNgu,

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("video/{id}")]
        public async Task<IActionResult> Edit(VideoEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await InitParam.Db.Video.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.FkNgonNgu = model.NgonNguId;
            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.HinhAnh = model.HinhAnh;
            dbItem.DuongDanFile = model.DuongDanFile;

            dbItem.NgaySua = DateTime.Now;
            dbItem.UserNguoiSua = "admin";

            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        [HttpGet]
        [Route("tao-moi-video")]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;

            return View(new VideoCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi-video")]
        public async Task<IActionResult> Create(VideoCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = new Video();

            dbItem.FkNgonNgu = model.NgonNguId;
            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.HinhAnh = model.HinhAnh;
            dbItem.DuongDanFile = model.DuongDanFile;

            dbItem.NgayTao = DateTime.Now;
            dbItem.NguoiTao = "admin";
            dbItem.NgaySua = DateTime.Now;
            dbItem.UserNguoiSua = "admin";

            InitParam.Db.Video.Add(dbItem);
            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = dbItem.Id });
        }











        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await InitParam.Db.Video
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
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
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
                InitParam.Db.Add(video);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", video.FkNgonNgu);
            return View(video);
        }
      
        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await InitParam.Db.Video
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
            var video = await InitParam.Db.Video.FindAsync(id);
            InitParam.Db.Video.Remove(video);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return InitParam.Db.Video.Any(e => e.Id == id);
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
    }
}
