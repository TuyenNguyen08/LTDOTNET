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
    public class TinTucsController : ControllerBase
    {
        private List<LoaiTin> ListLoaiTin { get; set; }
        public TinTucsController(InitParam initParam) : base(initParam)
        {
            var listLoaiTin = InitParam.Db.LoaiTin.AsNoTracking()
                .OrderBy(h => h.TenLoai)
                .Select(h => new LoaiTin
                {
                    Id = h.Id,
                    TenLoai = h.TenLoai
                }).ToList();

            ListLoaiTin = listLoaiTin;
        }

        [HttpGet]
        [Route("tin-tuc")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> TinTuc_Read([DataSourceRequest]DataSourceRequest request)
        {
            var resultDb = await InitParam.Db.TinTuc
                .Include(h => h.FkLoaiTinNavigation)
                .Include(h => h.FkNgonNguNavigation)
                .Include(h => h.FkUserNguoiSuaNavigation)
                .Include(h => h.FkUserNguoiTaoNavigation)
                .OrderBy(h => h.FkLoaiTinNavigation.TenLoai)
                .ThenBy(h => h.TieuDe)
                .ThenBy(h => h.FkNgonNguNavigation.TenNgonNgu)
                .ThenBy(h => h.Author)
                .Select(h => new TinTucIndexViewModel
                {
                    Id = h.Id,

                    TenLoaiId = h.FkLoaiTin,
                    TenLoai = h.FkLoaiTinNavigation.TenLoai,

                    TieuDe = h.TieuDe,
                    GioiThieu = h.GioiThieu,
                    Xem = h.LuotXem,
                    TacGia = h.Author,

                    NgonNguId = h.FkNgonNgu,
                    NgonNgu = h.FkNgonNguNavigation.TenNgonNgu,

                    NgayTao = h.NgayTao,
                    NgaySua = h.NgaySua,

                    NguoiSuaId = h.FkUserNguoiSua,
                    NguoiSua = h.FkUserNguoiSuaNavigation.HoVaTen,

                    NguoiTaoId = h.FkUserNguoiTao,
                    NguoiTao = h.FkUserNguoiTaoNavigation.HoVaTen
                })
                .ToListAsync();
            var result = await resultDb.ToDataSourceResultAsync(request);
            return Json(result);
        }

        [HttpGet]
        [Route("tin-tuc/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;
            ViewBag.ListLoaiTin = ListLoaiTin;

            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var model = await InitParam.Db.TinTuc.AsNoTracking()
               .Include(h => h.FkUserNguoiSuaNavigation)
               .Include(h => h.FkUserNguoiTaoNavigation)
               .Where(h => h.Id == id.Value)
               .Select(h => new TinTucEditViewModel
               {
                   Id = h.Id,

                   TenLoaiId = h.FkLoaiTin,

                   TieuDe = h.TieuDe,
                   GioiThieu = h.GioiThieu,
                   Xem = h.LuotXem,
                   HinhAnh = h.HinhAnhMinhHoa,
                   NoiDung = h.NoiDung,
                   TacGia = h.Author,

                   NgonNguId = h.FkNgonNgu,

                   NgayTao = h.NgayTao,
                   NgaySua = h.NgaySua,

                   NguoiSua = h.FkUserNguoiSuaNavigation.HoVaTen,

                   NguoiTao = h.FkUserNguoiTaoNavigation.HoVaTen

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
        [Route("tin-tuc/{id}")]
        public async Task<IActionResult> Edit(TinTucEditViewModel model)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;
            ViewBag.ListLoaiTin = ListLoaiTin;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = await InitParam.Db.TinTuc.FirstOrDefaultAsync(h => h.Id == model.Id);
            if (dbItem == null)
            {
                return NotFound();
            }

            dbItem.FkNgonNgu = model.NgonNguId;
            dbItem.FkLoaiTin = model.TenLoaiId;
            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.HinhAnhMinhHoa = model.HinhAnh;
            dbItem.NoiDung = model.NoiDung;
            dbItem.Author = model.TacGia;

            dbItem.NgaySua = DateTime.Now;
            dbItem.FkUserNguoiSua = "admin";

            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        [HttpGet]
        [Route("tao-moi-tin-tuc")]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;
            ViewBag.ListLoaiTin = ListLoaiTin;

            return View(new TinTucCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("tao-moi-tin-tuc")]
        public async Task<IActionResult> Create(TinTucCreateViewModel model)
        {
            ViewBag.ListNgonNgu = ListNgonNgu;
            ViewBag.ListLoaiTin = ListLoaiTin;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbItem = new TinTuc();

            dbItem.FkNgonNgu = model.TenLoaiId;
            dbItem.FkLoaiTin = model.NgonNguId;
            dbItem.TieuDe = model.TieuDe;
            dbItem.GioiThieu = model.GioiThieu;
            dbItem.NoiDung = model.NoiDung;
            dbItem.HinhAnhMinhHoa = model.HinhAnh;
            dbItem.Author = model.TacGia;

            dbItem.NgayTao = DateTime.Now;
            dbItem.FkUserNguoiTao = "admin";
            dbItem.NgaySua = dbItem.NgayTao;
            dbItem.FkUserNguoiSua = "admin";

            InitParam.Db.TinTuc.Add(dbItem);
            await InitParam.Db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = dbItem.Id });
        }




























        // GET: TinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await InitParam.Db.TinTuc
                .Include(t => t.FkLoaiTinNavigation)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserNguoiSuaNavigation)
                .Include(t => t.FkUserNguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // GET: TinTucs/Create
        public async Task<IActionResult> Create()
        {

            #region Get list master
            var listNgonNgu = await GetListNgonNguAsync();
            ViewBag.ListNgonNgu = listNgonNgu;
            #endregion
            var listLoaiTin = await GetListLoaiTinAsync();
            ViewBag.ListLoaiTin = listLoaiTin;


            ViewData["FkLoaiTin"] = new SelectList(InitParam.Db.LoaiTin, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
            ViewData["FkUserNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
            ViewData["FkUserNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName");

            return View();
        }

        // POST: TinTucs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiTin,NgayTao,FkUserNguoiTao,NoiDung,Stt,LuotXem,Author,FkUserNguoiSua,NgaySua")] TinTuc tinTuc)
        {

            if (ModelState.IsValid)
            {
                #region Get list master
                var listNgonNgu = await GetListNgonNguAsync();
                ViewBag.ListNgonNgu = listNgonNgu;
                #endregion
                var listLoaiTin = await GetListLoaiTinAsync();
                ViewBag.ListLoaiTin = listLoaiTin;
                InitParam.Db.Add(tinTuc);
                await InitParam.Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkLoaiTin"] = new SelectList(InitParam.Db.LoaiTin, "Id", "Id", tinTuc.FkLoaiTin);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", tinTuc.FkNgonNgu);
            ViewData["FkUserNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tinTuc.FkUserNguoiSua);
            ViewData["FkUserNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tinTuc.FkUserNguoiTao);
            return View(tinTuc);
        }


        private Task<List<DropdownlistViewModel>> GetListNgonNguAsync()
        {
            var list = InitParam.Db.NgonNgu.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenNgonNgu
                }).ToListAsync();

            return list;
        }
        private Task<List<DropdownlistViewModel>> GetListLoaiTinAsync()
        {
            var list = InitParam.Db.LoaiTin.AsNoTracking()
                .Select(h => new DropdownlistViewModel
                {
                    Id = h.Id,
                    Ten = h.TenLoai
                }).ToListAsync();

            return list;
        }
        // GET: TinTucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await InitParam.Db.TinTuc
                .Include(t => t.FkLoaiTinNavigation)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserNguoiSuaNavigation)
                .Include(t => t.FkUserNguoiTaoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // POST: TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinTuc = await InitParam.Db.TinTuc.FindAsync(id);
            InitParam.Db.TinTuc.Remove(tinTuc);
            await InitParam.Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinTucExists(int id)
        {
            return InitParam.Db.TinTuc.Any(e => e.Id == id);
        }
    }
}
