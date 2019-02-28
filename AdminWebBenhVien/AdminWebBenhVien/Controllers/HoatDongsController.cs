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
    public class HoatDongsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public HoatDongsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: HoatDongs
        [HttpGet]
        [Route("hoat-dong")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> HoatDong_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.HoatDong
                .Include(h => h.FkLoaiHoatDongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkNguoiSuaNavigation)
                .Include(h => h.FkNguoiTaoNavigation)
                .OrderBy(h => h.FkLoaiHoatDongNavigation.TenLoai)
                .ThenBy(h => h.TieuDe)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .ThenBy(h => h.Author)
                .Select(h => new TinTucIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkLoaiHoatDong,
                    TenLoai = h.FkLoaiHoatDongNavigation.TenLoai,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,
                    TacGia = h.Author,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgaySua,

                    NguoiSuaId = h.FkNguoiSua,
                    NguoiSua = h.FkNguoiSuaNavigation.HoVaTen,

                    NguoiTaoId = h.FkNguoiTao,
                    NguoiTao = h.FkNguoiTaoNavigation.HoVaTen
                })
                .ToListAsync();
            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: HoatDongs/Edit/5
        [HttpGet]
        [Route("hoat-dong/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.HoatDong.AsNoTracking()
                .Include(h => h.FkLoaiHoatDongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkNguoiSuaNavigation)
                .Include(h => h.FkNguoiTaoNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new HoatDongEditViewModel
                {
                    Id = h.Id,
                    
                    TenLoai = h.FkLoaiHoatDongNavigation.TenLoai,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,
                    TacGia = h.Author,
                    NoiDung = h.NoiDung,

                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgaySua,
                    
                    NguoiSua = h.FkNguoiSuaNavigation.HoVaTen,
                    
                    NguoiTao = h.FkNguoiTaoNavigation.HoVaTen
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
        [Route("hoat-dong/{id}")]
        public async Task<IActionResult> Edit(HoatDongEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.HoatDong.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.NoiDung = model.NoiDung;
            dbItem.Author = model.TacGia;

            dbItem.NgaySua = DateTime.Now;
            dbItem.FkNguoiSua = "admin";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }


























        // GET: HoatDongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDong = await _context.HoatDong
                .Include(h => h.FkLoaiHoatDongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkNguoiSuaNavigation)
                .Include(h => h.FkNguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoatDong == null)
            {
                return NotFound();
            }

            return View(hoatDong);
        }

        // GET: HoatDongs/Create
        public async Task<IActionResult> Create()
        {
            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            var listLoaiHoatDong = await GetListLoaiHoatDongAsync();
            ViewBag.ListLoaiHoatDong = listLoaiHoatDong;
            ViewData["FkLoaiHoatDong"] = new SelectList(_context.LoaiHoatDong, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            ViewData["FkNguoiSua"] = new SelectList(_context.User, "UserName", "UserName");
            ViewData["FkNguoiTao"] = new SelectList(_context.User, "UserName", "UserName");
            return View();
        }

        // POST: HoatDongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiHoatDong,NoiDung,Stt,LuotXem,FkNguoiTao,NgayTao,Author,FkNguoiSua,NgaySua")] HoatDong hoatDong)
        {
            if (ModelState.IsValid)
            {
                #region Get list master
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                #endregion
                var listLoaiHoatDong = await GetListLoaiHoatDongAsync();
                ViewBag.ListLoaiHoatDong = listLoaiHoatDong;
                _context.Add(hoatDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiHoatDong"] = new SelectList(_context.LoaiHoatDong, "Id", "Id", hoatDong.FkLoaiHoatDong);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", hoatDong.FkNgonNgu);
            ViewData["FkNguoiSua"] = new SelectList(_context.User, "UserName", "UserName", hoatDong.FkNguoiSua);
            ViewData["FkNguoiTao"] = new SelectList(_context.User, "UserName", "UserName", hoatDong.FkNguoiTao);
            return View(hoatDong);
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
        private Task<List<DropdownlistViewModel>> GetListLoaiHoatDongAsync()
        {
            var list = _context.LoaiHoatDong.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten=h.TenLoai
                }).ToListAsync();

            return list;
        }
        // GET: HoatDongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDong = await _context.HoatDong
                .Include(h => h.FkLoaiHoatDongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkNguoiSuaNavigation)
                .Include(h => h.FkNguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoatDong == null)
            {
                return NotFound();
            }

            return View(hoatDong);
        }

        // POST: HoatDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoatDong = await _context.HoatDong.FindAsync(id);
            _context.HoatDong.Remove(hoatDong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoatDongExists(int id)
        {
            return _context.HoatDong.Any(e => e.Id == id);
        }
    }
}