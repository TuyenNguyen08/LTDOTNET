using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;
using Microsoft.AspNetCore.Http;

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
            var fkNgonNgu = base.NgonNgu;

            var lsSLide = await InitParam.Db.SlideShow.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .Select(t => new SlideShow
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    HinhAnh = t.HinhAnh,

                })
                .ToListAsync();
            model.lsSLide = new List<SlideShow>(lsSLide);


            var listKhoaPhong = await InitParam.Db.KhoaPhong.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .Select(t => new KhoaPhong
                {
                    Id = t.Id,
                    TieuDeKhoa = t.TieuDeKhoa,
                })
                .ToListAsync();
            model.lsKhoaPhong = new List<KhoaPhong>(listKhoaPhong);


            var listGioiThieu = await InitParam.Db.GioiThieuChiTiet.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .Select(t => new GioiThieuChiTiet
                {
                    Id = t.Id,
                    Ten = t.Ten,
                })
                .ToListAsync();
            model.lsGioiThieu = new List<GioiThieuChiTiet>(listGioiThieu);


            var listTinTuc = await InitParam.Db.TinTuc.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .OrderByDescending(h => h.NgaySua)
                .ThenByDescending(h => h.NgayTao)
                .Take(3)
                .Select(t => new TinTuc
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    NgayTao = t.NgayTao,
                    HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                    GioiThieu = t.GioiThieu.Substring(0, 100) + "...",
                }).ToListAsync();
            model.lsTinTuc = new List<TinTuc>(listTinTuc);


            var listHoatDong = await InitParam.Db.HoatDong.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .OrderByDescending(h => h.NgaySua)
                .ThenByDescending(h => h.NgayTao)
                .Take(3)
                .Select(t => new HoatDong
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    NgayTao = t.NgayTao,
                    HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                    GioiThieu = t.GioiThieu.Substring(0, 100) + "...",
                })
                .ToListAsync();
            model.lsHoatDong = new List<HoatDong>(listHoatDong);


            var listVideo = await InitParam.Db.Video.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .OrderByDescending(h => h.NgaySua)
                .ThenByDescending(h => h.NgayTao)
                .Take(6)
                .Select(t => new Video
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    GioiThieu = t.GioiThieu,
                    HinhAnh = t.HinhAnh,
                    DuongDanFile = t.DuongDanFile,
                })
                .ToListAsync();
            model.lsVideo = new List<Video>(listVideo);


            var listSubNote = await InitParam.Db.SubNote.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .Take(4)
                .Select(t => new SubNote
                {
                    Id = t.Id,
                    TieuDe = t.TieuDe,
                    NoiDung = t.NoiDung.Substring(0, 200) + "...",
                    Image = t.Image,
                })
                .ToListAsync();
            model.lsSubNote = new List<SubNote>(listSubNote);


            var listSubPhone = await InitParam.Db.SubPhone.AsNoTracking()
                .Where(h => h.FkNgonNgu == fkNgonNgu)
                .Take(3)
                .Select(t => new SubPhone
                {
                    Id = t.Id,
                    Name = t.Name,
                    SoDienThoai = t.SoDienThoai,

                })
                .ToListAsync();
            model.lsSubPhone = new List<SubPhone>(listSubPhone);

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

        // Intro.aspx? ID = 1
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

        [HttpPost]
        public async Task<IActionResult> NgonNgu(int ngonNguId)
        {
            if (_listNgonNgu.Any(h => h.Id == ngonNguId))
            {
                base.NgonNgu = ngonNguId;
            }
            else if (_listNgonNgu.Count > 0)
            {
                base.NgonNgu = _listNgonNgu[0].Id;
            }
            else
            {
                base.NgonNgu = -1;
            }

            ViewBag.NgonNgu = base.NgonNgu;
            InitParam.HttpContextAccessor.HttpContext.Session.SetInt32("NgonNgu", base.NgonNgu);

            return Json(true);
        }
    }
}
