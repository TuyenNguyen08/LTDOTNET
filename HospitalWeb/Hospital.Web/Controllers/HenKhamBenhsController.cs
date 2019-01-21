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
    public class HenKhamBenhsController : ControllerBase
    {
        public HenKhamBenhsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: HenKhamBenhs
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = InitParam.Db.HenKhamBenh.Include(h => h.FkBacSiNavigation).Include(h => h.FkChuyenKhoaNavigation).Include(h => h.FkGioHenNavigation).Include(h => h.FkNamSinhNavigation).Include(h => h.FkQuocTichNavigation).Include(h => h.FkTinhTrangHonNhanNavigation).Include(h => h.FkTrangThaiNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }

        // GET: HenKhamBenhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var henKhamBenh = await InitParam.Db.HenKhamBenh
                .Include(h => h.FkBacSiNavigation)
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkGioHenNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkQuocTichNavigation)
                .Include(h => h.FkTinhTrangHonNhanNavigation)
                .Include(h => h.FkTrangThaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (henKhamBenh == null)
            {
                return NotFound();
            }

            return View(henKhamBenh);
        }

        // GET: HenKhamBenhs/Create
        public IActionResult Create()
        {
            ViewData["FkBacSi"] = new SelectList(InitParam.Db.DanhMucBacSi, "Id", "Id");
            ViewData["FkChuyenKhoa"] = new SelectList(InitParam.Db.PhongKham, "Id", "Id");
            ViewData["FkGioHen"] = new SelectList(InitParam.Db.GioKham, "Id", "Id");
            ViewData["FkNamSinh"] = new SelectList(InitParam.Db.NamSinh, "Id", "Id");
            ViewData["FkQuocTich"] = new SelectList(InitParam.Db.QuocTich, "Id", "Id");
            ViewData["FkTinhTrangHonNhan"] = new SelectList(InitParam.Db.TinhTrangHonNhan, "Id", "Id");
            ViewData["FkTrangThai"] = new SelectList(InitParam.Db.TrangThai, "Id", "Id");
            return View();
        }

        // POST: HenKhamBenhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkChuyenKhoa,FkBacSi,NgayHen,FkGioHen,MoTaTrieuChung,NgayGui,DiaChiEmail,HoVaTen,FkNamSinh,GioiTinh,FkTinhTrangHonNhan,FkQuocTich,SoDienThoaiNha,SoDienThoaiDiDong,DiaChi,BacSi,FkTrangThai")] HenKhamBenh henKhamBenh)
        {
            if (ModelState.IsValid)
            {
                InitParam.Db.Add(henKhamBenh);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBacSi"] = new SelectList(InitParam.Db.DanhMucBacSi, "Id", "Id", henKhamBenh.FkBacSi);
            ViewData["FkChuyenKhoa"] = new SelectList(InitParam.Db.PhongKham, "Id", "Id", henKhamBenh.FkChuyenKhoa);
            ViewData["FkGioHen"] = new SelectList(InitParam.Db.GioKham, "Id", "Id", henKhamBenh.FkGioHen);
            ViewData["FkNamSinh"] = new SelectList(InitParam.Db.NamSinh, "Id", "Id", henKhamBenh.FkNamSinh);
            ViewData["FkQuocTich"] = new SelectList(InitParam.Db.QuocTich, "Id", "Id", henKhamBenh.FkQuocTich);
            ViewData["FkTinhTrangHonNhan"] = new SelectList(InitParam.Db.TinhTrangHonNhan, "Id", "Id", henKhamBenh.FkTinhTrangHonNhan);
            ViewData["FkTrangThai"] = new SelectList(InitParam.Db.TrangThai, "Id", "Id", henKhamBenh.FkTrangThai);
            return View(henKhamBenh);
        }

        // GET: HenKhamBenhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var henKhamBenh = await InitParam.Db.HenKhamBenh.FindAsync(id);
            if (henKhamBenh == null)
            {
                return NotFound();
            }
            ViewData["FkBacSi"] = new SelectList(InitParam.Db.DanhMucBacSi, "Id", "Id", henKhamBenh.FkBacSi);
            ViewData["FkChuyenKhoa"] = new SelectList(InitParam.Db.PhongKham, "Id", "Id", henKhamBenh.FkChuyenKhoa);
            ViewData["FkGioHen"] = new SelectList(InitParam.Db.GioKham, "Id", "Id", henKhamBenh.FkGioHen);
            ViewData["FkNamSinh"] = new SelectList(InitParam.Db.NamSinh, "Id", "Id", henKhamBenh.FkNamSinh);
            ViewData["FkQuocTich"] = new SelectList(InitParam.Db.QuocTich, "Id", "Id", henKhamBenh.FkQuocTich);
            ViewData["FkTinhTrangHonNhan"] = new SelectList(InitParam.Db.TinhTrangHonNhan, "Id", "Id", henKhamBenh.FkTinhTrangHonNhan);
            ViewData["FkTrangThai"] = new SelectList(InitParam.Db.TrangThai, "Id", "Id", henKhamBenh.FkTrangThai);
            return View(henKhamBenh);
        }

        // POST: HenKhamBenhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FkChuyenKhoa,FkBacSi,NgayHen,FkGioHen,MoTaTrieuChung,NgayGui,DiaChiEmail,HoVaTen,FkNamSinh,GioiTinh,FkTinhTrangHonNhan,FkQuocTich,SoDienThoaiNha,SoDienThoaiDiDong,DiaChi,BacSi,FkTrangThai")] HenKhamBenh henKhamBenh)
        {
            if (id != henKhamBenh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    InitParam.Db.Update(henKhamBenh);
                    await InitParam.Db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HenKhamBenhExists(henKhamBenh.Id))
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
            ViewData["FkBacSi"] = new SelectList(InitParam.Db.DanhMucBacSi, "Id", "Id", henKhamBenh.FkBacSi);
            ViewData["FkChuyenKhoa"] = new SelectList(InitParam.Db.PhongKham, "Id", "Id", henKhamBenh.FkChuyenKhoa);
            ViewData["FkGioHen"] = new SelectList(InitParam.Db.GioKham, "Id", "Id", henKhamBenh.FkGioHen);
            ViewData["FkNamSinh"] = new SelectList(InitParam.Db.NamSinh, "Id", "Id", henKhamBenh.FkNamSinh);
            ViewData["FkQuocTich"] = new SelectList(InitParam.Db.QuocTich, "Id", "Id", henKhamBenh.FkQuocTich);
            ViewData["FkTinhTrangHonNhan"] = new SelectList(InitParam.Db.TinhTrangHonNhan, "Id", "Id", henKhamBenh.FkTinhTrangHonNhan);
            ViewData["FkTrangThai"] = new SelectList(InitParam.Db.TrangThai, "Id", "Id", henKhamBenh.FkTrangThai);
            return View(henKhamBenh);
        }

        // GET: HenKhamBenhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var henKhamBenh = await InitParam.Db.HenKhamBenh
                .Include(h => h.FkBacSiNavigation)
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkGioHenNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkQuocTichNavigation)
                .Include(h => h.FkTinhTrangHonNhanNavigation)
                .Include(h => h.FkTrangThaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (henKhamBenh == null)
            {
                return NotFound();
            }

            return View(henKhamBenh);
        }

        // POST: HenKhamBenhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var henKhamBenh = await InitParam.Db.HenKhamBenh.FindAsync(id);
            InitParam.Db.HenKhamBenh.Remove(henKhamBenh);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HenKhamBenhExists(int id)
        {
            return InitParam.Db.HenKhamBenh.Any(e => e.Id == id);
        }
    }
}
