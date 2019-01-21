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
    public class HomesController : ControllerBase
    {

        public HomesController(InitParam initParam) : base(initParam)
        {

        }

        // GET: HomeIndex
        public async Task<IActionResult> Index()
        {
            HomeModel model = new HomeModel();

            var lsSLide = await InitParam.Db.SlideShow.AsNoTracking()
                .Select(t => new SlideShow
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    HinhAnh = t.HinhAnh,

                }).ToListAsync();
            model.lsSLide = new List<SlideShow>(lsSLide);

            var listKhoaPhong = await InitParam.Db.KhoaPhong.AsNoTracking().Select(t => new KhoaPhong
            {
                Id = t.Id,
                TieuDeKhoa = t.TieuDeKhoa,
            }).ToListAsync();


            model.lsKhoaPhong = new List<KhoaPhong>(listKhoaPhong);

            var listGioiThieu = await InitParam.Db.GioiThieuChiTiet.AsNoTracking().Select(t => new GioiThieuChiTiet
            {
                Id = t.Id,
                Ten = t.Ten,
            }).ToListAsync();


            model.lsGioiThieu = new List<GioiThieuChiTiet>(listGioiThieu);

            var listTinTuc = await InitParam.Db.TinTuc.AsNoTracking().Take(3).Select(t => new TinTuc
            {
                Id = t.Id,
                TieuDe = t.TieuDe,
                NgayTao = t.NgayTao,
            }).ToListAsync();


            model.lsTinTuc = new List<TinTuc>(listTinTuc);


            return View(model);


        }

        // GET: KhoaPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaPhong = await InitParam.Db.KhoaPhong.AsNoTracking()
                .Include(k => k.FkLoaiKhoaPhongNavigation)
                .Include(k => k.FkNgonNguNavigation)
                .Where(m => m.Id == id)
                .Select(t => new KhoaPhong
                {
                    Id = t.Id,
                    TieuDeKhoa = t.TieuDeKhoa,
                    TenKhoaPhong = t.TenKhoaPhong,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    FkLoaiKhoaPhongNavigation = t.FkLoaiKhoaPhongNavigation
                }).FirstOrDefaultAsync();

            ;

            if (khoaPhong == null)
            {
                return NotFound();
            }

            return View(khoaPhong);
        }




        // GET: KhoaPhongs/Create
        public async Task<IActionResult> Contact()
        {
            var nBenhVien7CContext = InitParam.Db.HenKhamBenh;
            return View(await nBenhVien7CContext.ToListAsync());
        }
        public async Task<IActionResult> About(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await InitParam.Db.GioiThieuChiTiet.AsNoTracking()
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkGioiThieuNavigation)
                .Where(t => t.Id == id)
                .Select(t => new GioiThieuChiTiet
                {
                    Ten = t.Ten,
                    GioiThieu = t.GioiThieu,
                    NoiDung = t.NoiDung,
                    HinhAnh = t.HinhAnh,
                }).FirstOrDefaultAsync();
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }
    }
}
