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
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.HoatDong.Include(h => h.FkLoaiHoatDongNavigation).Include(h => h.FkNgonNguNavigation).Include(h => h.FkNguoiSuaNavigation).Include(h => h.FkNguoiTaoNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> HoatDong_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listHoatDong = _context.HoatDong.AsNoTracking()
                .Select(t => new HoatDongViewModel
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                    FkNgonNgu = t.FkNgonNgu,
                    FkLoaiHoatDong = t.FkLoaiHoatDong,
                    NoiDung = t.NoiDung,
                    Stt = t.Stt,
                    LuotXem = t.LuotXem,
                    FkNguoiTao = t.FkNguoiTao,
                    NgayTao = t.NgayTao,
                    Author = t.Author,
                    FkNguoiSua = t.FkNguoiSua,
                    NgaySua = t.NgaySua
                });
            var result = await listHoatDong.ToDataSourceResultAsync(request);
            return Json(result);
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

        // GET: HoatDongs/Edit/5
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
            var listLoaiHoatDong = await GetListLoaiHoatDongAsync();
            ViewBag.ListLoaiHoatDong = listLoaiHoatDong;
            var hoatDong = await _context.HoatDong.FindAsync(id);
            if (hoatDong == null)
            {
                return NotFound();
            }
            ViewData["FkLoaiHoatDong"] = new SelectList(_context.LoaiHoatDong, "Id", "Id", hoatDong.FkLoaiHoatDong);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", hoatDong.FkNgonNgu);
            ViewData["FkNguoiSua"] = new SelectList(_context.User, "UserName", "UserName", hoatDong.FkNguoiSua);
            ViewData["FkNguoiTao"] = new SelectList(_context.User, "UserName", "UserName", hoatDong.FkNguoiTao);
            return View(hoatDong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiHoatDong,NoiDung,Stt,LuotXem,FkNguoiTao,NgayTao,Author,FkNguoiSua,NgaySua")] HoatDong hoatDong)
        {
            if (id != hoatDong.Id)
            {
                return NotFound();
            }
            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            var listLoaiHoatDong = await GetListLoaiHoatDongAsync();
            ViewBag.ListLoaiHoatDong = listLoaiHoatDong;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoatDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoatDongExists(hoatDong.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
              //  return RedirectToAction(nameof(Index));
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