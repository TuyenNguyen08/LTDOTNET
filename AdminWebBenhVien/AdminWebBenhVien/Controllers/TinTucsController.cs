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
    public class TinTucsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public TinTucsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: TinTucs
        [HttpGet]
        [Route("tin-tuc")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> TinTuc_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.TinTuc
                .Include(h => h.FkLoaiTinNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserNguoiSuaNavigation)
                .Include(h => h.FkUserNguoiTaoNavigation)
                .OrderBy(h => h.FkLoaiTinNavigation.TenLoai)
                .ThenBy(h => h.TieuDe)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .ThenBy(h => h.Author)
                .Select(h => new TinTucIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkLoaiTin,
                    TenLoai = h.FkLoaiTinNavigation.TenLoai,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,
                    TacGia = h.Author,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgaySua,

                    NguoiSuaId = h.FkUserNguoiSua,
                    NguoiSua = h.FkUserNguoiSuaNavigation.HoVaTen,

                    NguoiTaoId = h.FkUserNguoiTao,
                    NguoiTao = h.FkUserNguoiTaoNavigation.HoVaTen

                })
                .ToListAsync();
            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: TinTucs/Edit/5
        [HttpGet]
        [Route("tin-tuc/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await _context.TinTuc.AsNoTracking()
               .Include(h => h.FkLoaiTinNavigation)
               .Include(h => h.FkNgonNguNavigation)
               .Include(h => h.FkUserNguoiSuaNavigation)
               .Include(h => h.FkUserNguoiTaoNavigation)
               .Where(h => h.Id == id.Value)
               .Select(h => new TinTucEditViewModel
               {
                   Id = h.Id,
                   
                   TenLoai = h.FkLoaiTinNavigation.TenLoai,

                   TieuDe = h.TieuDe,
                   GioiThieu = h.GioiThieu,
                   Xem = h.LuotXem,
                   NoiDung = h.NoiDung,
                   TacGia = h.Author,
                   
                   NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                   NgayTao = h.NgayTao,
                   NgaySua = h.NgaySua,
                   
                   NguoiSua = h.FkUserNguoiSuaNavigation.HoVaTen,
                   
                   NguoiTao = h.FkUserNguoiTaoNavigation.HoVaTen

               })
               .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: TinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tin-tuc/{id}")]
        public async Task<IActionResult> Edit(TinTucEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await _context.TinTuc.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.NoiDung = model.NoiDung;
            dbItem.Author = model.TacGia;

            dbItem.NgaySua = DateTime.Now;
            dbItem.FkUserNguoiSua = "admin";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }





























        // GET: TinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTuc
                .Include(t => t.FkLoaiTinNavigation)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserNguoiSuaNavigation)
                .Include(t => t.FkUserNguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // GET: TinTucs/Create
        public async Task<IActionResult> Create()
        {
          
            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            var listLoaiTin = await GetListLoaiTinAsync();
            ViewBag.ListLoaiTin = listLoaiTin;
           

            ViewData["FkLoaiTin"] = new SelectList(_context.LoaiTin, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            ViewData["FkUserNguoiSua"] = new SelectList(_context.User, "UserName", "UserName");
            ViewData["FkUserNguoiTao"] = new SelectList(_context.User, "UserName", "UserName");

            return View();
        }

        // POST: TinTucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiTin,NgayTao,FkUserNguoiTao,NoiDung,Stt,LuotXem,Author,FkUserNguoiSua,NgaySua")] TinTuc tinTuc)
        {
           
            if (ModelState.IsValid)
            {
                #region Get list master
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                #endregion
                var listLoaiTin = await GetListLoaiTinAsync();
                ViewBag.ListLoaiTin = listLoaiTin;
                _context.Add(tinTuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiTin"] = new SelectList(_context.LoaiTin, "Id", "Id", tinTuc.FkLoaiTin);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", tinTuc.FkNgonNgu);
            ViewData["FkUserNguoiSua"] = new SelectList(_context.User, "UserName", "UserName", tinTuc.FkUserNguoiSua);
            ViewData["FkUserNguoiTao"] = new SelectList(_context.User, "UserName", "UserName", tinTuc.FkUserNguoiTao);
            return View(tinTuc);
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
        private Task<List<DropdownlistViewModel>> GetListLoaiTinAsync()
        {
            var list = _context.LoaiTin.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenLoai
                }).ToListAsync();

            return list;
        }
        // GET: TinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await _context.TinTuc
                .Include(t => t.FkLoaiTinNavigation)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserNguoiSuaNavigation)
                .Include(t => t.FkUserNguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // POST: TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await _context.TinTuc.FindAsync(id);
            _context.TinTuc.Remove(tinTuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return _context.TinTuc.Any(e => e.Id == id);
        }
    }
}
