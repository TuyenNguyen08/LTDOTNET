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
    public class NoiDungTinhChiTietsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public NoiDungTinhChiTietsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: NoiDungTinhChiTiets
        [Route("benh-nhan")]
        public  IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> NoiDungTinhChiTiet_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.NoiDungTinhChiTiet
                 .Include(h => h.FkNoiDungTinhNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserChinhsuaNavigation)
                .Select(h => new BenhNhanIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkNoiDungTinh,
                    TenLoai = h.FkNoiDungTinhNavigation.TenNoiDung,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgaySua = h.NgayChinhSua,
                    NguoiSuaId = h.FkUserChinhsua,
                    NguoiSua = h.FkUserChinhsuaNavigation.HoVaTen

                }).ToListAsync();
            var resultJson = await resultDb.ToDataSourceResultAsync(request);
            return Json(resultJson);
        }
        // GET: NoiDungTinhChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noiDungTinhChiTiet = await _context.NoiDungTinhChiTiet
                .Include(n => n.FkNgonNguNavigation)
                .Include(n => n.FkNoiDungTinhNavigation)
                .Include(n => n.FkUserChinhsuaNavigation)
                .FirstOrDefaultAsync(m => m.FkNgonNgu == id);
            if (noiDungTinhChiTiet == null)
            {
                return NotFound();
            }

            return View(noiDungTinhChiTiet);
        }

        // GET: NoiDungTinhChiTiets/Create
        public IActionResult Create()
        {
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            ViewData["FkNoiDungTinh"] = new SelectList(_context.NoiDungTinh, "Id", "Id");
            ViewData["FkUserChinhsua"] = new SelectList(_context.User, "UserName", "UserName");
            return View();
        }

        // POST: NoiDungTinhChiTiets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FkNgonNgu,FkNoiDungTinh,TieuDe,GioiThieu,NoiDung,HinhAnh,NgayChinhSua,FkUserChinhsua,Id,LuotXem")] NoiDungTinhChiTiet noiDungTinhChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noiDungTinhChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(_context.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(_context.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
            return View(noiDungTinhChiTiet);
        }

        // GET: NoiDungTinhChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;

            var listNoiDungTinh = await GetListNoiDungTinhAsync();
            ViewBag.ListNoiDungTinh = listNoiDungTinh;
            var noiDungTinhChiTiet = await _context.NoiDungTinhChiTiet.Where(t=>t.Id==id).FirstOrDefaultAsync();
            if (noiDungTinhChiTiet == null)
            {
                return NotFound();
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(_context.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(_context.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
            return View(noiDungTinhChiTiet);
        }

        // POST: NoiDungTinhChiTiets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FkNgonNgu,FkNoiDungTinh,TieuDe,GioiThieu,NoiDung,HinhAnh,NgayChinhSua,FkUserChinhsua,Id,LuotXem")] NoiDungTinhChiTiet noiDungTinhChiTiet)
        {
            if (id != noiDungTinhChiTiet.FkNgonNgu)
            {
                return NotFound();
            }
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;

            var listNoiDungTinh = await GetListNoiDungTinhAsync();
            ViewBag.ListNoiDungTinh = listNoiDungTinh;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noiDungTinhChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoiDungTinhChiTietExists(noiDungTinhChiTiet.FkNgonNgu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             //   return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(_context.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(_context.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
            return View(noiDungTinhChiTiet);
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

        private Task<List<DropdownlistViewModel>> GetListNoiDungTinhAsync()
        {
            var list = _context.NoiDungTinh.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenNoiDung
                }).ToListAsync();

            return list;
        }
        // GET: NoiDungTinhChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noiDungTinhChiTiet = await _context.NoiDungTinhChiTiet
                .Include(n => n.FkNgonNguNavigation)
                .Include(n => n.FkNoiDungTinhNavigation)
                .Include(n => n.FkUserChinhsuaNavigation)
                .FirstOrDefaultAsync(m => m.FkNgonNgu == id);
            if (noiDungTinhChiTiet == null)
            {
                return NotFound();
            }

            return View(noiDungTinhChiTiet);
        }

        // POST: NoiDungTinhChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noiDungTinhChiTiet = await _context.NoiDungTinhChiTiet.FindAsync(id);
            _context.NoiDungTinhChiTiet.Remove(noiDungTinhChiTiet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoiDungTinhChiTietExists(int id)
        {
            return _context.NoiDungTinhChiTiet.Any(e => e.FkNgonNgu == id);
        }
    }
}
