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
    public class DichVuChiTietsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public DichVuChiTietsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: DichVuChiTiets
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.DichVuChiTiet.Include(d => d.FkDichVuNavigation).Include(d => d.FkNgonNguNavigation).Include(d => d.FkUserModifyNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> DichVuChiTiet_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listDichVuChiTiet = _context.DichVuChiTiet.AsNoTracking()
                .Select(t => new DichVuChiTietViewModel
                {
                    Id = t.Id,
                    FkNgonNgu = t.FkNgonNgu,
                    FkDichVu=t.FkDichVu,
                    TenDichVu=t.TenDichVu,
                    GioiThieu = t.GioiThieu.Substring(0, 40) + "...",
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                    FkUserModify = t.FkUserModify,
                    NgayTao = t.NgayTao,
                    NgayChinhSua = t.NgayChinhSua,
                    LuotXem = t.LuotXem
                });
            var result = await listDichVuChiTiet.ToDataSourceResultAsync(request);
            return Json(result);
        }
       
        // GET: DichVuChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVuChiTiet = await _context.DichVuChiTiet.Where(t=>t.Id==id).FirstOrDefaultAsync();
            if (dichVuChiTiet == null)
            {
                return NotFound();
            }
            ViewData["FkDichVu"] = new SelectList(_context.DichVu, "Id", "Id", dichVuChiTiet.FkDichVu);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", dichVuChiTiet.FkNgonNgu);
            ViewData["FkUserModify"] = new SelectList(_context.User, "UserName", "UserName", dichVuChiTiet.FkUserModify);
            return View(dichVuChiTiet);
        }

        // POST: DichVuChiTiets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FkNgonNgu,FkDichVu,TenDichVu,GioiThieu,NoiDung,HinhAnh,FkUserModify,NgayTao,NgayChinhSua,Id,LuotXem")] DichVuChiTiet dichVuChiTiet)
        {
            if (id != dichVuChiTiet.FkNgonNgu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  //  _context.Update(dichVuChiTiet);
                    _context.Update(dichVuChiTiet).Property(x => x.Id).IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuChiTietExists(dichVuChiTiet.FkNgonNgu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
           //     return RedirectToAction(nameof(Index));
            }
            ViewData["FkDichVu"] = new SelectList(_context.DichVu, "Id", "Id", dichVuChiTiet.FkDichVu);
            ViewData["FkNgonNgu"] = new SelectList(_context.NgonNgu, "Id", "Id", dichVuChiTiet.FkNgonNgu);
            ViewData["FkUserModify"] = new SelectList(_context.User, "UserName", "UserName", dichVuChiTiet.FkUserModify);
            return View(dichVuChiTiet);
        }

       
        private bool DichVuChiTietExists(int id)
        {
            return _context.DichVuChiTiet.Any(e => e.FkNgonNgu == id);
        }
    }
}
