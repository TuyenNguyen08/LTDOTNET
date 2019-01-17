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
    public class DanhMucBacSisController : ControllerBase
    {

        public DanhMucBacSisController (InitParam initParam) : base(initParam)
        {
           
        }

        // GET: DanhMucBacSis
        public async Task<IActionResult> Index()
        {
            var lstdanhMucBacSi = await InitParam.Db.DanhMucBacSi.AsNoTracking()
                .Include(d => d.FkKhoaPhongNavigation)
              .Select(t => new DanhMucBacSi
              {
                  Id=t.Id,
                  TenBacSi=t.TenBacSi,
              }).ToListAsync(); ;
            return View(lstdanhMucBacSi);
        }

        // GET: DanhMucBacSis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucBacSi = await InitParam.Db.DanhMucBacSi
                .Include(d => d.FkKhoaPhongNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhMucBacSi == null)
            {
                return NotFound();
            }

            return View(danhMucBacSi);
        }

        // GET: DanhMucBacSis/Create
        public IActionResult Create()
        {
            ViewData["FkKhoaPhong"] = new SelectList(InitParam.Db.KhoaPhong, "Id", "Id");
            return View();
        }

        // POST: DanhMucBacSis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenBacSi,FkKhoaPhong")] DanhMucBacSi danhMucBacSi)
        {
            if (ModelState.IsValid)
            {
                InitParam.Db.Add(danhMucBacSi);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkKhoaPhong"] = new SelectList(InitParam.Db.KhoaPhong, "Id", "Id", danhMucBacSi.FkKhoaPhong);
            return View(danhMucBacSi);
        }

        // GET: DanhMucBacSis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucBacSi = await InitParam.Db.DanhMucBacSi.FindAsync(id);
            if (danhMucBacSi == null)
            {
                return NotFound();
            }
            ViewData["FkKhoaPhong"] = new SelectList(InitParam.Db.KhoaPhong, "Id", "Id", danhMucBacSi.FkKhoaPhong);
            return View(danhMucBacSi);
        }

        // POST: DanhMucBacSis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenBacSi,FkKhoaPhong")] DanhMucBacSi danhMucBacSi)
        {
            if (id != danhMucBacSi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    InitParam.Db.Update(danhMucBacSi);
                    await InitParam.Db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucBacSiExists(danhMucBacSi.Id))
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
            ViewData["FkKhoaPhong"] = new SelectList(InitParam.Db.KhoaPhong, "Id", "Id", danhMucBacSi.FkKhoaPhong);
            return View(danhMucBacSi);
        }

        // GET: DanhMucBacSis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMucBacSi = await InitParam.Db.DanhMucBacSi
                .Include(d => d.FkKhoaPhongNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (danhMucBacSi == null)
            {
                return NotFound();
            }

            return View(danhMucBacSi);
        }

        // POST: DanhMucBacSis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMucBacSi = await InitParam.Db.DanhMucBacSi.FindAsync(id);
            InitParam.Db.DanhMucBacSi.Remove(danhMucBacSi);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucBacSiExists(int id)
        {
            return InitParam.Db.DanhMucBacSi.Any(e => e.Id == id);
        }
    }
}
