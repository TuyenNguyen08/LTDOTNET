using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;

namespace Hospital.Web.Controllers
{
    public class DichVusController : ControllerBase
    {

        public DichVusController(InitParam initParam) : base(initParam)
        {
        }

        // GET: DichVus
        public async Task<IActionResult> Index()
        {
            var lstDichVuChiTiet = await InitParam.Db.DichVuChiTiet.AsNoTracking()
                .Select(t => new DichVuChiTiet
                {
                   Id = t.Id,
                   TenDichVu=t.TenDichVu,
                }).ToListAsync();

            return View(lstDichVuChiTiet);
        }
        // GET: DichVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lstdichVuChiTiet = await InitParam.Db.DichVuChiTiet.AsNoTracking()
                .Include(t => t.FkDichVu)
                .Include(t => t.FkDichVuNavigation)
                .Include(t => t.FkNgonNgu)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserModify)
                .Include(t => t.FkUserModifyNavigation)
                .Where(t => t.Id == id)
                .Select(t => new DichVuChiTiet
                {
                    Id = t.Id,
                    TenDichVu = t.TenDichVu,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                    NgayTao = t.NgayTao,
                    LuotXem = t.LuotXem,
                }).FirstOrDefaultAsync();

            if (lstdichVuChiTiet == null)
            {
                return NotFound();
            }

            return View(lstdichVuChiTiet);
        }

        // GET: DichVus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DichVus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MoTa")] DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                InitParam.Db.Add(dichVu);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dichVu);
        }

        // GET: DichVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await InitParam.Db.DichVu.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            return View(dichVu);
        }

        // POST: DichVus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MoTa")] DichVu dichVu)
        {
            if (id != dichVu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    InitParam.Db.Update(dichVu);
                    await InitParam.Db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuExists(dichVu.Id))
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
            return View(dichVu);
        }

        // GET: DichVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await InitParam.Db.DichVu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // POST: DichVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dichVu = await InitParam.Db.DichVu.FindAsync(id);
            InitParam.Db.DichVu.Remove(dichVu);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DichVuExists(int id)
        {
            return InitParam.Db.DichVu.Any(e => e.Id == id);
        }
    }
}
