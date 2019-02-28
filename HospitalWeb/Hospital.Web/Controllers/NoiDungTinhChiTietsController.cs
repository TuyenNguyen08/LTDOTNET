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
    public class NoiDungTinhChiTietsController : ControllerBase
    {
        public NoiDungTinhChiTietsController(InitParam initParam) : base(initParam)
        {
        }

        // GET: NoiDungTinhChiTiets
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = InitParam.Db.NoiDungTinhChiTiet.Include(n => n.FkNgonNguNavigation).Include(n => n.FkNoiDungTinhNavigation).Include(n => n.FkUserChinhsuaNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }

        // GET: NoiDungTinhChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noiDungTinhChiTiet = await InitParam.Db.NoiDungTinhChiTiet.AsNoTracking()
                .Include(t => t.FkNgonNgu)
                .Include(t => t.FkNoiDungTinh)
                .Include(t => t.FkUserChinhsua)
                .Where(t => t.Id == id)
                .Select(t => new NoiDungTinhChiTiet
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                }).FirstOrDefaultAsync();
            if (noiDungTinhChiTiet == null)
            {
                return NotFound();
            }
            #region listNoiDungTinh

            var listNoiDungTinh = await InitParam.Db.NoiDungTinh.AsNoTracking().Take(9).Select(t => new NoiDungTinh
            {
                Id = t.Id,
                TenNoiDung=t.TenNoiDung

            }).ToListAsync();

            ViewBag.lsNoiDungTinh = listNoiDungTinh;

            #endregion


            return View(noiDungTinhChiTiet);
        }

        // GET: NoiDungTinhChiTiets/Create
        public IActionResult Create()
        {
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
            ViewData["FkNoiDungTinh"] = new SelectList(InitParam.Db.NoiDungTinh, "Id", "Id");
            ViewData["FkUserChinhsua"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
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
                InitParam.Db.Add(noiDungTinhChiTiet);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(InitParam.Db.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
            return View(noiDungTinhChiTiet);
        }

        // GET: NoiDungTinhChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noiDungTinhChiTiet = await InitParam.Db.NoiDungTinhChiTiet.FindAsync(id);
            if (noiDungTinhChiTiet == null)
            {
                return NotFound();
            }
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(InitParam.Db.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
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
                    InitParam.Db.Update(noiDungTinhChiTiet);
                    await InitParam.Db.SaveChangesAsync();
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
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", noiDungTinhChiTiet.FkNgonNgu);
            ViewData["FkNoiDungTinh"] = new SelectList(InitParam.Db.NoiDungTinh, "Id", "Id", noiDungTinhChiTiet.FkNoiDungTinh);
            ViewData["FkUserChinhsua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", noiDungTinhChiTiet.FkUserChinhsua);
            return View(noiDungTinhChiTiet);
        }

        // GET: NoiDungTinhChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noiDungTinhChiTiet = await InitParam.Db.NoiDungTinhChiTiet
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
            var noiDungTinhChiTiet = await InitParam.Db.NoiDungTinhChiTiet.FindAsync(id);
            InitParam.Db.NoiDungTinhChiTiet.Remove(noiDungTinhChiTiet);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoiDungTinhChiTietExists(int id)
        {
            return InitParam.Db.NoiDungTinhChiTiet.Any(e => e.FkNgonNgu == id);
        }
    }
}
