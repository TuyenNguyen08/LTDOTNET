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
    public class HenKhamBenhsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public HenKhamBenhsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: HenKhamBenhs
        public async Task<IActionResult> Index()
        {
            var nBenhVien7CContext = _context.HenKhamBenh.Include(h => h.FkBacSiNavigation).Include(h => h.FkChuyenKhoaNavigation).Include(h => h.FkGioHenNavigation).Include(h => h.FkNamSinhNavigation).Include(h => h.FkQuocTichNavigation).Include(h => h.FkTinhTrangHonNhanNavigation).Include(h => h.FkTrangThaiNavigation);
            return View(await nBenhVien7CContext.ToListAsync());
        }
        public async Task<IActionResult> HenKhamBenh_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listHoatDong = _context.HenKhamBenh.AsNoTracking()
                .Select(t => new HenKhamBenhViewModel
                {
                    Id = t.Id,
                    FkChuyenKhoa=t.FkChuyenKhoa,
                    FkBacSi=t.FkBacSi,
                    NgayHen=t.NgayHen,
                    FkGioHen=t.FkGioHen,
                    MoTaTrieuChung=t.MoTaTrieuChung,
                    NgayGui=t.NgayGui,
                    DiaChiEmail=t.DiaChiEmail,
                    HoVaTen=t.HoVaTen,
                    FkNamSinh=t.FkNamSinh,
                    GioiTinh=t.GioiTinh,
                    FkTinhTrangHonNhan=t.FkTinhTrangHonNhan,
                    FkQuocTich=t.FkQuocTich,
                    SoDienThoaiDiDong=t.SoDienThoaiDiDong,
                    SoDienThoaiNha=t.SoDienThoaiNha,
                    DiaChi=t.DiaChi,
                    BacSi=t.BacSi,
                    FkTrangThai=t.FkTrangThai
                   
                });
            var result = await listHoatDong.ToDataSourceResultAsync(request);
            return Json(result);
        }
        // GET: HenKhamBenhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var henKhamBenh = await _context.HenKhamBenh
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
            ViewData["FkBacSi"] = new SelectList(_context.DanhMucBacSi, "Id", "Id");
            ViewData["FkChuyenKhoa"] = new SelectList(_context.PhongKham, "Id", "Id");
            ViewData["FkGioHen"] = new SelectList(_context.GioKham, "Id", "Id");
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id");
            ViewData["FkQuocTich"] = new SelectList(_context.QuocTich, "Id", "Id");
            ViewData["FkTinhTrangHonNhan"] = new SelectList(_context.TinhTrangHonNhan, "Id", "Id");
            ViewData["FkTrangThai"] = new SelectList(_context.TrangThai, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkChuyenKhoa,FkBacSi,NgayHen,FkGioHen,MoTaTrieuChung,NgayGui,DiaChiEmail,HoVaTen,FkNamSinh,GioiTinh,FkTinhTrangHonNhan,FkQuocTich,SoDienThoaiNha,SoDienThoaiDiDong,DiaChi,BacSi,FkTrangThai")] HenKhamBenh henKhamBenh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(henKhamBenh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBacSi"] = new SelectList(_context.DanhMucBacSi, "Id", "Id", henKhamBenh.FkBacSi);
            ViewData["FkChuyenKhoa"] = new SelectList(_context.PhongKham, "Id", "Id", henKhamBenh.FkChuyenKhoa);
            ViewData["FkGioHen"] = new SelectList(_context.GioKham, "Id", "Id", henKhamBenh.FkGioHen);
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id", henKhamBenh.FkNamSinh);
            ViewData["FkQuocTich"] = new SelectList(_context.QuocTich, "Id", "Id", henKhamBenh.FkQuocTich);
            ViewData["FkTinhTrangHonNhan"] = new SelectList(_context.TinhTrangHonNhan, "Id", "Id", henKhamBenh.FkTinhTrangHonNhan);
            ViewData["FkTrangThai"] = new SelectList(_context.TrangThai, "Id", "Id", henKhamBenh.FkTrangThai);
            return View(henKhamBenh);
        }

        // GET: HenKhamBenhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var henKhamBenh = await _context.HenKhamBenh
               .Where(t => t.Id == id).FirstOrDefaultAsync();

            var listChuyenKhoa = await GetListChuyenKhoaAsync();
            ViewBag.ListChuyenKhoa = listChuyenKhoa;

            var listBacSi = await GetListBacSiAsync();
            ViewBag.ListBacSi = listBacSi;

            var listGioHen = await GetListGioHenAsync();
            ViewBag.ListGioHen = listGioHen;

            var listNamSinh = await GetListNamSinhAsync();
            ViewBag.ListNamSinh = listNamSinh;

            var listTinhTrangHonNhan = await GetListTinhTrangHonNhanAsync();
            ViewBag.ListTinhTrangHonNhan = listTinhTrangHonNhan;

            var listTrangThai = await GetListTrangThaiAsync();
            ViewBag.ListTrangThai = listTrangThai;

            var listQuocTich = await GetListQuocTichAsync();
            ViewBag.ListQuocTich = listQuocTich;




            if (henKhamBenh == null)
            {
                return NotFound();
            }
            ViewData["FkBacSi"] = new SelectList(_context.DanhMucBacSi, "Id", "Id", henKhamBenh.FkBacSi);
            ViewData["FkChuyenKhoa"] = new SelectList(_context.PhongKham, "Id", "Id", henKhamBenh.FkChuyenKhoa);
            ViewData["FkGioHen"] = new SelectList(_context.GioKham, "Id", "Id", henKhamBenh.FkGioHen);
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id", henKhamBenh.FkNamSinh);
            ViewData["FkQuocTich"] = new SelectList(_context.QuocTich, "Id", "Id", henKhamBenh.FkQuocTich);
            ViewData["FkTinhTrangHonNhan"] = new SelectList(_context.TinhTrangHonNhan, "Id", "Id", henKhamBenh.FkTinhTrangHonNhan);
            ViewData["FkTrangThai"] = new SelectList(_context.TrangThai, "Id", "Id", henKhamBenh.FkTrangThai);
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
            var listChuyenKhoa = await GetListChuyenKhoaAsync();
            ViewBag.ListChuyenKhoa = listChuyenKhoa;

            var listBacSi = await GetListBacSiAsync();
            ViewBag.ListBacSi = listBacSi;

            var listGioHen = await GetListGioHenAsync();
            ViewBag.ListGioHen = listGioHen;

            var listNamSinh = await GetListNamSinhAsync();
            ViewBag.ListNamSinh = listNamSinh;

            var listTinhTrangHonNhan = await GetListTinhTrangHonNhanAsync();
            ViewBag.ListTinhTrangHonNhan = listTinhTrangHonNhan;

            var listTrangThai = await GetListTrangThaiAsync();
            ViewBag.ListTrangThai = listTrangThai;

            var listQuocTich = await GetListQuocTichAsync();
            ViewBag.ListQuocTich = listQuocTich;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(henKhamBenh);
                    await _context.SaveChangesAsync();
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
              //  return RedirectToAction(nameof(Index));
            }
            ViewData["FkBacSi"] = new SelectList(_context.DanhMucBacSi, "Id", "Id", henKhamBenh.FkBacSi);
            ViewData["FkChuyenKhoa"] = new SelectList(_context.PhongKham, "Id", "Id", henKhamBenh.FkChuyenKhoa);
            ViewData["FkGioHen"] = new SelectList(_context.GioKham, "Id", "Id", henKhamBenh.FkGioHen);
            ViewData["FkNamSinh"] = new SelectList(_context.NamSinh, "Id", "Id", henKhamBenh.FkNamSinh);
            ViewData["FkQuocTich"] = new SelectList(_context.QuocTich, "Id", "Id", henKhamBenh.FkQuocTich);
            ViewData["FkTinhTrangHonNhan"] = new SelectList(_context.TinhTrangHonNhan, "Id", "Id", henKhamBenh.FkTinhTrangHonNhan);
            ViewData["FkTrangThai"] = new SelectList(_context.TrangThai, "Id", "Id", henKhamBenh.FkTrangThai);
            return View(henKhamBenh);
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

        private Task<List<DropdownlistViewModel>> GetListBacSiAsync()
        {
            var list = _context.DanhMucBacSi.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenBacSi
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListGioHenAsync()
        {
            var list = _context.GioKham.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.Gio
                }).ToListAsync();

            return list;
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

        private Task<List<DropdownlistViewModel>> GetListTinhTrangHonNhanAsync()
        {
            var list = _context.TinhTrangHonNhan.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TinhTrang
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListTrangThaiAsync()
        {
            var list = _context.TrangThai.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenLoaiTrangThai
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListQuocTichAsync()
        {
            var list = _context.QuocTich.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenQuocGia
                }).ToListAsync();

            return list;
        }
        // GET: HenKhamBenhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var henKhamBenh = await _context.HenKhamBenh
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
            var henKhamBenh = await _context.HenKhamBenh.FindAsync(id);
            _context.HenKhamBenh.Remove(henKhamBenh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HenKhamBenhExists(int id)
        {
            return _context.HenKhamBenh.Any(e => e.Id == id);
        }
    }
}
