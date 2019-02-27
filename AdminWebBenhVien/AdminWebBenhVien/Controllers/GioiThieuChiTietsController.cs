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
    public class GioiThieuChiTietsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public GioiThieuChiTietsController(NBenhVien7CContext context)
        {
            _context = context;
        }


        [Route("gioi-thieu-tong-quan")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GioiThieuChiTiet_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await _context.GioiThieuChiTiet
                .Include(h => h.FkGioiThieuNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserModifyNavigation)
                .OrderBy(h => h.FkGioiThieuNavigation.TenGioiThieu)
                .ThenBy(h => h.Ten)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .Select(h => new GioiThieuTongQuanIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkGioiThieu,
                    TenLoai = h.FkGioiThieuNavigation.TenGioiThieu,

                    TieuDe = h.Ten,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgayChinhSua,
                    NguoiSuaId = h.FkUserModify,
                    NguoiSua = h.FkUserModifyNavigation.HoVaTen
                })
                .ToListAsync();

            var resultJson = await resultDb.ToDataSourceResultAsync(request);
            return Json(resultJson);
        }

        // GET: GioiThieuChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieuChiTiet = await _context.GioiThieuChiTiet
                .Include(g => g.FkGioiThieuNavigation)
                .Include(g => g.FkNgonNguNavigation)
                .Include(g => g.FkUserModifyNavigation)
                .FirstOrDefaultAsync(m => m.FkNgonNgu == id);
            if (gioiThieuChiTiet == null)
            {
                return NotFound();
            }

            return View(gioiThieuChiTiet);
        }

        // GET: GioiThieuChiTiets/Create
        public IActionResult Create()
        {
            ViewData["FkGioiThieu"] = new SelectList(_context.GioiThieu, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id");
            ViewData["FkUserModify"] = new SelectList(_context.User, "UserName", "UserName");
            return View();
        }

        // POST: GioiThieuChiTiets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FkNgonNgu,FkGioiThieu,Ten,GioiThieu,NoiDung,HinhAnh,FkUserModify,NgayTao,NgayChinhSua,Id,LuotXem")] GioiThieuChiTiet gioiThieuChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioiThieuChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkGioiThieu"] = new SelectList(_context.GioiThieu, "Id", "Id", gioiThieuChiTiet.FkGioiThieu);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", gioiThieuChiTiet.FkNgonNgu);
            ViewData["FkUserModify"] = new SelectList(_context.User, "UserName", "UserName", gioiThieuChiTiet.FkUserModify);
            return View(gioiThieuChiTiet);
        }

        // GET: GioiThieuChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieuChiTiet = await _context.GioiThieuChiTiet.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (gioiThieuChiTiet == null)
            {
                return NotFound();
            }
            ViewData["FkGioiThieu"] = new SelectList(_context.GioiThieu, "Id", "Id", gioiThieuChiTiet.FkGioiThieu);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", gioiThieuChiTiet.FkNgonNgu);
            ViewData["FkUserModify"] = new SelectList(_context.User, "UserName", "UserName", gioiThieuChiTiet.FkUserModify);
            return View(gioiThieuChiTiet);
        }

        // POST: GioiThieuChiTiets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FkNgonNgu,FkGioiThieu,Ten,GioiThieu,NoiDung,HinhAnh,FkUserModify,NgayTao,NgayChinhSua,Id,LuotXem")] GioiThieuChiTiet gioiThieuChiTiet)
        {
            if (id != gioiThieuChiTiet.FkNgonNgu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //  _context.Update(gioiThieuChiTiet);
                    _context.Update(gioiThieuChiTiet).Property(x => x.Id).IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioiThieuChiTietExists(gioiThieuChiTiet.FkNgonNgu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // return RedirectToAction(nameof(Index));
            }
            ViewData["FkGioiThieu"] = new SelectList(_context.GioiThieu, "Id", "Id", gioiThieuChiTiet.FkGioiThieu);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", gioiThieuChiTiet.FkNgonNgu);
            ViewData["FkUserModify"] = new SelectList(_context.User, "UserName", "UserName", gioiThieuChiTiet.FkUserModify);
            return View(gioiThieuChiTiet);
        }

        // GET: GioiThieuChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioiThieuChiTiet = await _context.GioiThieuChiTiet
                .Include(g => g.FkGioiThieuNavigation)
                .Include(g => g.FkNgonNguNavigation)
                .Include(g => g.FkUserModifyNavigation)
                .FirstOrDefaultAsync(m => m.FkNgonNgu == id);
            if (gioiThieuChiTiet == null)
            {
                return NotFound();
            }

            return View(gioiThieuChiTiet);
        }

        // POST: GioiThieuChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioiThieuChiTiet = await _context.GioiThieuChiTiet.FindAsync(id);
            _context.GioiThieuChiTiet.Remove(gioiThieuChiTiet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioiThieuChiTietExists(int id)
        {
            return _context.GioiThieuChiTiet.Any(e => e.FkNgonNgu == id);
        }
    }
}
