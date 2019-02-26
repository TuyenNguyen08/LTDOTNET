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
    public class HoiDapYkhoasController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public HoiDapYkhoasController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: HoiDapYkhoas
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.HoiDapYkhoa.Include(h => h.FkChuyenKhoaNavigation).Include(h => h.FkNamSinhNavigation).Include(h => h.FkUserTraLoiNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        public async Task<IActionResult> HoiDapYKhoa_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listGioiThieuChiTiet = _context.HoiDapYkhoa.AsNoTracking()
                .Select(t => new HoiDapYKhoaViewModel
                {
                    Id=t.Id,
                    TieuDe=t.TieuDe,
                    NoiDungCauHoi=t.NoiDungCauHoi.Substring(0, 40) + "...",
                    HoTenNguoiHoi=t.HoTenNguoiHoi,
                    GioiTinh=t.GioiTinh,
                    FkNamSinh=t.FkNamSinh,
                    NoiDungTraLoi=t.NoiDungTraLoi,
                    NgayGuiCauHoi=t.NgayGuiCauHoi,
                    NgayTraLoi=t.NgayTraLoi,
                    IsShow=t.IsShow,
                    FkChuyenKhoa=t.FkChuyenKhoa,
                    FkUserTraLoi=t.FkUserTraLoi,
                    DaTraLoi=t.DaTraLoi,
                    Email=t.Email,
                    DiaChi=t.DiaChi,
                    SoDienThoai=t.SoDienThoai
                });
            var result = await listGioiThieuChiTiet.ToDataSourceResultAsync(request);
            return Json(result);
        }
        // GET: HoiDapYkhoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoiDapYkhoa = await _context.HoiDapYkhoa
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkUserTraLoiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoiDapYkhoa == null)
            {
                return NotFound();
            }

            return View(hoiDapYkhoa);
        }

        // GET: HoiDapYkhoas/Create
        public async Task<IActionResult> Create()
        {
            var listNamSinh = await GetListNamSinhAsync();
            ViewBag.ListNamSinh = listNamSinh;
            var listChuyenKhoa = await GetListChuyenKhoaAsync();
            ViewBag.ListChuyenKhoa = listChuyenKhoa;

            ViewData["FkChuyenKhoa"] = new SelectList(_context.LoaiCauHoi, "Id", "Id");
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id");
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName");
            return View();
        }

        // POST: HoiDapYkhoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,NoiDungCauHoi,HoTenNguoiHoi,GioiTinh,FkNamSinh,NoiDungTraLoi,NgayGuiCauHoi,NgayTraLoi,NguoiTraLoi,IsShow,FkChuyenKhoa,FkUserTraLoi,DaTraLoi,Email,DiaChi,SoDienThoai")] HoiDapYkhoa hoiDapYkhoa)
        {
            if (ModelState.IsValid)
            {
                var listNamSinh = await GetListNamSinhAsync();
                ViewBag.ListNamSinh = listNamSinh;
                var listChuyenKhoa = await GetListChuyenKhoaAsync();
                ViewBag.ListChuyenKhoa = listChuyenKhoa;

                _context.Add(hoiDapYkhoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkChuyenKhoa"] = new SelectList(_context.LoaiCauHoi, "Id", "Id", hoiDapYkhoa.FkChuyenKhoa);
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id", hoiDapYkhoa.FkNamSinh);
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName", hoiDapYkhoa.FkUserTraLoi);
            return View(hoiDapYkhoa);
        }

        // GET: HoiDapYkhoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var listNamSinh = await GetListNamSinhAsync();
            ViewBag.ListNamSinh = listNamSinh;
            var listChuyenKhoa = await GetListChuyenKhoaAsync();
            ViewBag.ListChuyenKhoa = listChuyenKhoa;

            var hoiDapYkhoa = await _context.HoiDapYkhoa.FindAsync(id);
            if (hoiDapYkhoa == null)
            {
                return NotFound();
            }
            ViewData["FkChuyenKhoa"] = new SelectList(_context.LoaiCauHoi, "Id", "Id", hoiDapYkhoa.FkChuyenKhoa);
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id", hoiDapYkhoa.FkNamSinh);
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName", hoiDapYkhoa.FkUserTraLoi);
            return View(hoiDapYkhoa);
        }

        // POST: HoiDapYkhoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,NoiDungCauHoi,HoTenNguoiHoi,GioiTinh,FkNamSinh,NoiDungTraLoi,NgayGuiCauHoi,NgayTraLoi,NguoiTraLoi,IsShow,FkChuyenKhoa,FkUserTraLoi,DaTraLoi,Email,DiaChi,SoDienThoai")] HoiDapYkhoa hoiDapYkhoa)
        {
            if (id != hoiDapYkhoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var listNamSinh = await GetListNamSinhAsync();
                    ViewBag.ListNamSinh = listNamSinh;
                    var listChuyenKhoa = await GetListChuyenKhoaAsync();
                    ViewBag.ListChuyenKhoa = listChuyenKhoa;

                    _context.Update(hoiDapYkhoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoiDapYkhoaExists(hoiDapYkhoa.Id))
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
            ViewData["FkChuyenKhoa"] = new SelectList(_context.LoaiCauHoi, "Id", "Id", hoiDapYkhoa.FkChuyenKhoa);
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id", hoiDapYkhoa.FkNamSinh);
            ViewData["FkUserTraLoi"] = new SelectList(_context.User, "UserName", "UserName", hoiDapYkhoa.FkUserTraLoi);
            return View(hoiDapYkhoa);
        }

        // GET: HoiDapYkhoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoiDapYkhoa = await _context.HoiDapYkhoa
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkUserTraLoiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoiDapYkhoa == null)
            {
                return NotFound();
            }

            return View(hoiDapYkhoa);
        }

        // POST: HoiDapYkhoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoiDapYkhoa = await _context.HoiDapYkhoa.FindAsync(id);
            _context.HoiDapYkhoa.Remove(hoiDapYkhoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoiDapYkhoaExists(int id)
        {
            return _context.HoiDapYkhoa.Any(e => e.Id == id);
        }
        private Task<List<DropdownlistViewModel>> GetListNamSinhAsync()
        {
            var list = _context.NamSinh.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    NamSinh = h.Nam
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListChuyenKhoaAsync()
        {
            var list = _context.PhongKham.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenPhongKham
                }).ToListAsync();

            return list;
        }
    }
}
