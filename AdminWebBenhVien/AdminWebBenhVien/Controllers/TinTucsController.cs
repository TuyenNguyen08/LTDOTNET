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
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.TinTuc.Include(t => t.FkLoaiTinNavigation).Include(t => t.FkNgonNguNavigation).Include(t => t.FkUserNguoiSuaNavigation).Include(t => t.FkUserNguoiTaoNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> TinTuc_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listKhoaPhong = _context.TinTuc.AsNoTracking()
                .Select(t => new TinTucViewModel
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                    FkNgonNgu = t.FkNgonNgu,
                    FkLoaiTin = t.FkLoaiTin,
                    NgayTao = t.NgayTao,
                    FkUserNguoiTao = t.FkUserNguoiTao,
                    FkUserNguoiSua = t.FkUserNguoiSua,
                    NoiDung = t.NoiDung,
                    Stt = t.Stt,
                    LuotXem = t.LuotXem,
                    Author = t.Author,
                    NgaySua = t.NgaySua


                });
            var result = await listKhoaPhong.ToDataSourceResultAsync(request);
            return Json(result);
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

        // GET: TinTucs/Edit/5
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
            var listLoaiTin = await GetListLoaiTinAsync();
            ViewBag.ListLoaiTin = listLoaiTin;
            var tinTuc = await _context.TinTuc.FindAsync(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            ViewData["FkLoaiTin"] = new SelectList(_context.LoaiTin, "Id", "Id", tinTuc.FkLoaiTin);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", tinTuc.FkNgonNgu);
            ViewData["FkUserNguoiSua"] = new SelectList(_context.User, "UserName", "UserName", tinTuc.FkUserNguoiSua);
            ViewData["FkUserNguoiTao"] = new SelectList(_context.User, "UserName", "UserName", tinTuc.FkUserNguoiTao);
            return View(tinTuc);
        }

        // POST: TinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiTin,NgayTao,FkUserNguoiTao,NoiDung,Stt,LuotXem,Author,FkUserNguoiSua,NgaySua")] TinTuc tinTuc)
        {
            if (id != tinTuc.Id)
            {
                return NotFound();
            }
            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            var listLoaiTin = await GetListLoaiTinAsync();
            ViewBag.ListLoaiTin = listLoaiTin;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinTuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
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
