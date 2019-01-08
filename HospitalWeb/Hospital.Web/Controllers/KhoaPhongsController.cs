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
    public class KhoaPhongsController : ControllerBase
    {

        public KhoaPhongsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: KhoaPhongs
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = InitParam.Db.KhoaPhong.Include(k => k.FkLoaiKhoaPhongNavigation).Include(k => k.FkNgonNguNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }

        // GET: KhoaPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await InitParam.Db.KhoaPhong
                .Include(k => k.FkLoaiKhoaPhongNavigation)
                .Include(k => k.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoaPhong == null)
            {
                return NotFound();
            }

            return View(khoaPhong);
        }

        // GET: KhoaPhongs/Create
        public IActionResult Create()
        {
            ViewData["FkLoaiKhoaPhong"] = new SelectList(InitParam.Db.LoaiKhoaPhong, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
            return View();
        }

        // POST: KhoaPhongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDeKhoa,TenKhoaPhong,GioiThieu,NoiDung,NgayCapNhat,UserModify,FkLoaiKhoaPhong,FkNgonNgu,HinhAnhDaiDien,HenKhamBenh,Stt,LuotXem")] KhoaPhong khoaPhong)
        {
            if (ModelState.IsValid)
            {
                InitParam.Db.Add(khoaPhong);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiKhoaPhong"] = new SelectList(InitParam.Db.LoaiKhoaPhong, "Id", "Id", khoaPhong.FkLoaiKhoaPhong);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", khoaPhong.FkNgonNgu);
            return View(khoaPhong);
        }

        // GET: KhoaPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await InitParam.Db.KhoaPhong.FindAsync(id);
            if (khoaPhong == null)
            {
                return NotFound();
            }
            ViewData["FkLoaiKhoaPhong"] = new SelectList(InitParam.Db.LoaiKhoaPhong, "Id", "Id", khoaPhong.FkLoaiKhoaPhong);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", khoaPhong.FkNgonNgu);
            return View(khoaPhong);
        }

        // POST: KhoaPhongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDeKhoa,TenKhoaPhong,GioiThieu,NoiDung,NgayCapNhat,UserModify,FkLoaiKhoaPhong,FkNgonNgu,HinhAnhDaiDien,HenKhamBenh,Stt,LuotXem")] KhoaPhong khoaPhong)
        {
            if (id != khoaPhong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    InitParam.Db.Update(khoaPhong);
                    await InitParam.Db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaPhongExists(khoaPhong.Id))
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
            ViewData["FkLoaiKhoaPhong"] = new SelectList(InitParam.Db.LoaiKhoaPhong, "Id", "Id", khoaPhong.FkLoaiKhoaPhong);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", khoaPhong.FkNgonNgu);
            return View(khoaPhong);
        }

        // GET: KhoaPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await InitParam.Db.KhoaPhong
                .Include(k => k.FkLoaiKhoaPhongNavigation)
                .Include(k => k.FkNgonNguNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (khoaPhong == null)
            {
                return NotFound();
            }

            return View(khoaPhong);
        }

        // POST: KhoaPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaPhong = await InitParam.Db.KhoaPhong.FindAsync(id);
            InitParam.Db.KhoaPhong.Remove(khoaPhong);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaPhongExists(int id)
        {
            return InitParam.Db.KhoaPhong.Any(e => e.Id == id);
        }
    }
}
