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
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.NoiDungTinhChiTiet.Include(n => n.FkNgonNguNavigation).Include(n => n.FkNoiDungTinhNavigation).Include(n => n.FkUserChinhsuaNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> NoiDungTinhChiTiet_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listNoiDungTinhChiTiet = _context.NoiDungTinhChiTiet.AsNoTracking()
                .Select(t => new NoiDungTinhChiTietViewModel
                {
                    Id = t.Id,
                    FkNgonNgu = t.FkNgonNgu,
                    FkNoiDungTinh=t.FkNoiDungTinh,
                    TieuDe=t.TieuDe,
                    GioiThieu = t.GioiThieu.Substring(0, 40) + "...",
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                    NgayChinhSua = t.NgayChinhSua,
                    FkUserChinhsua=t.FkUserChinhsua,
                    LuotXem = t.LuotXem
                });
            var result = await listNoiDungTinhChiTiet.ToDataSourceResultAsync(request);
            return Json(result);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(_context.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(_context.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
            return View(noiDungTinhChiTiet);
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
