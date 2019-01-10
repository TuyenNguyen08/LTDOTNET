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
    public class HoatDongsController : ControllerBase
    {
        public HoatDongsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: HoatDongs
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = InitParam.Db.HoatDong
                .Include(h => h.FkLoaiHoatDongNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkNguoiSuaNavigation)
                .Include(h => h.FkNguoiTaoNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }

        // GET: HoatDongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDong = await InitParam.Db.HoatDong
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
        public IActionResult Create()
        {
            ViewData["FkLoaiHoatDong"] = new SelectList(InitParam.Db.LoaiHoatDong, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
            ViewData["FkNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
            ViewData["FkNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
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
                InitParam.Db.Add(hoatDong);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiHoatDong"] = new SelectList(InitParam.Db.LoaiHoatDong, "Id", "Id", hoatDong.FkLoaiHoatDong);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", hoatDong.FkNgonNgu);
            ViewData["FkNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", hoatDong.FkNguoiSua);
            ViewData["FkNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", hoatDong.FkNguoiTao);
            return View(hoatDong);
        }

        // GET: HoatDongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDong = await InitParam.Db.HoatDong.FindAsync(id);
            if (hoatDong == null)
            {
                return NotFound();
            }
            ViewData["FkLoaiHoatDong"] = new SelectList(InitParam.Db.LoaiHoatDong, "Id", "Id", hoatDong.FkLoaiHoatDong);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", hoatDong.FkNgonNgu);
            ViewData["FkNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", hoatDong.FkNguoiSua);
            ViewData["FkNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", hoatDong.FkNguoiTao);
            return View(hoatDong);
        }

        // POST: HoatDongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiHoatDong,NoiDung,Stt,LuotXem,FkNguoiTao,NgayTao,Author,FkNguoiSua,NgaySua")] HoatDong hoatDong)
        {
            if (id != hoatDong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    InitParam.Db.Update(hoatDong);
                    await InitParam.Db.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiHoatDong"] = new SelectList(InitParam.Db.LoaiHoatDong, "Id", "Id", hoatDong.FkLoaiHoatDong);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", hoatDong.FkNgonNgu);
            ViewData["FkNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", hoatDong.FkNguoiSua);
            ViewData["FkNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", hoatDong.FkNguoiTao);
            return View(hoatDong);
        }

        // GET: HoatDongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoatDong = await InitParam.Db.HoatDong
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
            var hoatDong = await InitParam.Db.HoatDong.FindAsync(id);
            InitParam.Db.HoatDong.Remove(hoatDong);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoatDongExists(int id)
        {
            return InitParam.Db.HoatDong.Any(e => e.Id == id);
        }
    }
}
