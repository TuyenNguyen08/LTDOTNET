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
    public class HenKhamBenhsController : ControllerBase
    {
        private List<NamSinh> ListNamSinh { get; set; }
        private List<GioKham> ListGioHen { get; set; }
        private List<PhongKham> ListChuyenKhoa { get; set; }

        private List<DanhMucBacSi> ListBacSi { get; set; }

        private List<TinhTrangHonNhan> ListTinhTrangHonNhan { get; set; }

        private List<TrangThai> ListTrangThai { get; set; }

        private List<QuocTich> ListQuocTich { get; set; }
        public HenKhamBenhsController(InitParam initParam) : base(initParam)
        {
            var listNamSinh = InitParam.Db.NamSinh.AsNoTracking()
                .OrderBy(h => h.Nam)
                .Select(h => new NamSinh
                {
                    Id = h.Id,
                    Nam = h.Nam
                }).ToList();

            ListNamSinh = listNamSinh;

            var listChuyenKhoa = InitParam.Db.PhongKham.AsNoTracking()
                .OrderBy(h => h.TenPhongKham)
                .Select(h => new PhongKham
                {
                    Id = h.Id,
                    TenPhongKham = h.TenPhongKham
                }).ToList();

            ListChuyenKhoa = listChuyenKhoa;

            var listGioHen = InitParam.Db.GioKham.AsNoTracking()
                .OrderBy(h => h.Gio)
                .Select(h => new GioKham
                {
                    Id = h.Id,
                    Gio = h.Gio
                }).ToList();

            ListGioHen = listGioHen;

            var listBacSi = InitParam.Db.DanhMucBacSi.AsNoTracking()
                .OrderBy(h => h.TenBacSi)
                .Select(h => new DanhMucBacSi
                {
                    Id = h.Id,
                    TenBacSi = h.TenBacSi
                }).ToList();

            ListBacSi = listBacSi;

            var listTinhTrangHonNhan = InitParam.Db.TinhTrangHonNhan.AsNoTracking()
                .OrderBy(h => h.TinhTrang)
                .Select(h => new TinhTrangHonNhan
                {
                    Id = h.Id,
                    TinhTrang = h.TinhTrang
                }).ToList();

            ListTinhTrangHonNhan = listTinhTrangHonNhan;

            var listTrangThai = InitParam.Db.TrangThai.AsNoTracking()
                .OrderBy(h => h.TenLoaiTrangThai)
                .Select(h => new TrangThai
                {
                    Id = h.Id,
                    TenLoaiTrangThai = h.TenLoaiTrangThai
                }).ToList();

            ListTrangThai = listTrangThai;

            var listQuocTich = InitParam.Db.QuocTich.AsNoTracking()
               .OrderBy(h => h.TenQuocGia)
               .Select(h => new QuocTich
               {
                   Id = h.Id,
                   TenQuocGia = h.TenQuocGia
               }).ToList();

            ListQuocTich = listQuocTich;
        }

        [HttpGet]
        [Route("dat-hen-lich-kham")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> HenKhamBenh_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await InitParam.Db.HenKhamBenh
                .Include(h => h.FkChuyenKhoaNavigation)
                .Include(h => h.FkBacSiNavigation)
                .Include(h => h.FkGioHenNavigation)
                .Include(h => h.FkNamSinhNavigation)
                .Include(h => h.FkQuocTichNavigation)
                .Include(h => h.FkTinhTrangHonNhan)
                .Include(h => h.FkTrangThaiNavigation)
                .OrderBy(h => h.FkChuyenKhoaNavigation.TenPhongKham)
                .ThenBy(h => h.FkBacSiNavigation.TenBacSi)
                .ThenBy(h => h.HoVaTen)
                .ThenBy(h => h.FkQuocTichNavigation.TenQuocGia)
                .Select(h => new HenKhamBenhIndexViewModel
                {
                    Id = h.Id,

                    ChuyenKhoaId = h.FkChuyenKhoa,
                    TenChuyenKhoa = h.FkChuyenKhoaNavigation.TenPhongKham,

                    BacSiId = h.FkBacSi,
                    TenBacSi = h.FkBacSiNavigation.TenBacSi,

                    NgayHen = h.NgayHen,

                    GioHenId = h.FkGioHen,
                    GioHen = h.FkGioHenNavigation.Gio,

                    MoTaTrieuChung = h.MoTaTrieuChung,
                    NgayGui = h.NgayGui,
                    DiaChiEmail = h.DiaChiEmail,
                    HoVaTen = h.HoVaTen,

                    NamSinhId = h.FkNamSinh,
                    NamSinh = h.FkNamSinhNavigation.Nam,

                    GioiTinh = h.GioiTinh,

                    TinhTrangHonNhanId = h.FkTinhTrangHonNhan,
                    TinhTrangHonNhan = h.FkTinhTrangHonNhanNavigation.TinhTrang,

                    QuocTichId = h.FkQuocTich,
                    QuocTich = h.FkQuocTichNavigation.TenQuocGia,

                    SoDienThoaiDiDong = h.SoDienThoaiDiDong,
                    SoDienThoaiNha = h.SoDienThoaiNha,
                    DiaChi = h.DiaChi,
                    BacSi = h.BacSi,

                    TrangThaiId = h.FkTrangThai,
                    TenTrangThai = h.FkTrangThaiNavigation.TenLoaiTrangThai
                   
                })
                .ToListAsync();
            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }


        [HttpGet]
        [Route("dat-hen-lich-kham/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ListGioHen = ListGioHen;
            ViewBag.ListChuyenKhoa = ListChuyenKhoa;
            ViewBag.ListNamSinh = ListNamSinh;
            ViewBag.ListBacSi = ListBacSi;
            ViewBag.ListTinhTrangHonNhan = ListTinhTrangHonNhan;
            ViewBag.ListTrangThai = ListTrangThai;

            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await InitParam.Db.HenKhamBenh.AsNoTracking()
                
                .Include(h => h.FkQuocTichNavigation)
                .Where(h => h.Id == id.Value)
                .Select(h => new HenKhamBenhEditViewModel
                {
                    Id = h.Id,

                    ChuyenKhoaId = h.FkChuyenKhoa,

                    BacSiId = h.FkBacSi,

                    NgayHen = h.NgayHen,

                    GioHenId = h.FkGioHen,

                    MoTaTrieuChung = h.MoTaTrieuChung,
                    NgayGui = h.NgayGui,
                    DiaChiEmail = h.DiaChiEmail,
                    HoVaTen = h.HoVaTen,

                    NamSinhId = h.FkNamSinh,

                    GioiTinh = h.GioiTinh,

                    TinhTrangHonNhanId = h.FkTinhTrangHonNhan,

                    QuocTichId = h.FkQuocTich,
                   

                    SoDienThoaiDiDong = h.SoDienThoaiDiDong,
                    SoDienThoaiNha = h.SoDienThoaiNha,
                    DiaChi = h.DiaChi,
                    BacSi = h.BacSi,

                    TrangThaiId = h.FkTrangThai,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("dat-hen-lich-kham/{id}")]
        public async Task<IActionResult> Edit(HenKhamBenhEditViewModel model)
        {

            ViewBag.ListGioHen = ListGioHen;
            ViewBag.ListChuyenKhoa = ListChuyenKhoa;
            ViewBag.ListNamSinh = ListNamSinh;
            ViewBag.ListTinhTrangHonNhan = ListTinhTrangHonNhan;
            ViewBag.ListTrangThai = ListTrangThai;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await InitParam.Db.HenKhamBenh.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.HoVaTen = model.HoVaTen;
            dbItem.DiaChi = model.DiaChi;
            dbItem.SoDienThoaiDiDong = model.SoDienThoaiDiDong;
            dbItem.SoDienThoaiNha = model.SoDienThoaiNha;
            dbItem.DiaChiEmail = model.DiaChiEmail;
            dbItem.FkNamSinh = model.NamSinhId;
            dbItem.GioiTinh = model.GioiTinh;
            dbItem.FkChuyenKhoa = model.ChuyenKhoaId;
            dbItem.NgayHen = model.NgayHen;
            dbItem.FkGioHen = model.GioHenId;
            dbItem.FkBacSi = model.BacSiId;
            dbItem.MoTaTrieuChung = model.MoTaTrieuChung;
            dbItem.FkTinhTrangHonNhan = model.TinhTrangHonNhanId;
            dbItem.FkQuocTich= model.QuocTichId;
            dbItem.FkTrangThai = model.TrangThaiId;

            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        [HttpGet]
        [Route("tao-moi-lich-hen")]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.ListGioHen = ListGioHen;
            ViewBag.ListChuyenKhoa = ListChuyenKhoa;
            ViewBag.ListNamSinh = ListNamSinh;
            ViewBag.ListTinhTrangHonNhan = ListTinhTrangHonNhan;
            ViewBag.ListTrangThai = ListTrangThai;

            return View(new HenKhamBenhCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi-lich-hen")]
        public async Task<IActionResult> Create(HenKhamBenhCreateViewModel model)
        {
            ViewBag.ListGioHen = ListGioHen;
            ViewBag.ListChuyenKhoa = ListChuyenKhoa;
            ViewBag.ListNamSinh = ListNamSinh;
            ViewBag.ListTinhTrangHonNhan = ListTinhTrangHonNhan;
            ViewBag.ListTrangThai = ListTrangThai;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = new HenKhamBenh();

            dbItem.HoVaTen = model.HoVaTen;
            dbItem.DiaChi = model.DiaChi;
            dbItem.SoDienThoaiDiDong = model.SoDienThoaiDiDong;
            dbItem.SoDienThoaiNha = model.SoDienThoaiNha;
            dbItem.DiaChiEmail = model.DiaChiEmail;
            dbItem.FkNamSinh = model.NamSinhId;
            dbItem.GioiTinh = model.GioiTinh;
            dbItem.FkChuyenKhoa = model.ChuyenKhoaId;
            dbItem.NgayHen = model.NgayHen;
            dbItem.FkGioHen = model.GioHenId;
            dbItem.FkBacSi = model.BacSiId;
            dbItem.MoTaTrieuChung = model.MoTaTrieuChung;
            dbItem.FkTinhTrangHonNhan = model.TinhTrangHonNhanId;
            dbItem.FkQuocTich = model.QuocTichId;
            dbItem.FkTrangThai = model.TrangThaiId;

            dbItem.NgayGui = DateTime.Now; 

            InitParam.Db.HenKhamBenh.Add(dbItem);
            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = dbItem.Id });
        }




































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
        public async Task<IActionResult> Create()
        {
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

            ViewData["FkBacSi"] = new SelectList(InitParam.Db.DanhMucBacSi, "Id", "Id");
            ViewData["FkChuyenKhoa"] = new SelectList(InitParam.Db.PhongKham, "Id", "Id");
            ViewData["FkGioHen"] = new SelectList(InitParam.Db.GioKham, "Id", "Id");
            ViewData["FkNamSinh"] = new SelectList(InitParam.Db.NamSinh, "Id", "Id");
            ViewData["FkQuocTich"] = new SelectList(InitParam.Db.QuocTich, "Id", "Id");
            ViewData["FkTinhTrangHonNhan"] = new SelectList(InitParam.Db.TinhTrangHonNhan, "Id", "Id");
            ViewData["FkTrangThai"] = new SelectList(InitParam.Db.TrangThai, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FkChuyenKhoa,FkBacSi,NgayHen,FkGioHen,MoTaTrieuChung,NgayGui,DiaChiEmail,HoVaTen,FkNamSinh,GioiTinh,FkTinhTrangHonNhan,FkQuocTich,SoDienThoaiNha,SoDienThoaiDiDong,DiaChi,BacSi,FkTrangThai")] HenKhamBenh henKhamBenh)
        {
            if (ModelState.IsValid)
            {
             
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
       
        private Task<List<DropdownlistViewModel>> GetListChuyenKhoaAsync()
        {
            var list = InitParam.Db.PhongKham.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenPhongKham
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListBacSiAsync()
        {
            var list = InitParam.Db.DanhMucBacSi.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenBacSi
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListGioHenAsync()
        {
            var list = InitParam.Db.GioKham.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.Gio
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListNamSinhAsync()
        {
            var list = InitParam.Db.NamSinh.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    NamSinh = h.Nam
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListTinhTrangHonNhanAsync()
        {
            var list = InitParam.Db.TinhTrangHonNhan.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TinhTrang
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListTrangThaiAsync()
        {
            var list = InitParam.Db.TrangThai.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenLoaiTrangThai
                }).ToListAsync();

            return list;
        }

        private Task<List<DropdownlistViewModel>> GetListQuocTichAsync()
        {
            var list = InitParam.Db.QuocTich.AsNoTracking()
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
