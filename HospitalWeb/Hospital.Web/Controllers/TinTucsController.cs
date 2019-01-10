using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Web.EfModels;
using Hospital.Web.Models;
using X.PagedList;

namespace Hospital.Web.Controllers
{
    public class TinTucsController : ControllerBase
    {
        public TinTucsController(InitParam initParam) : base(initParam)
        {

        }

        // GET: TinTucs
        public async Task<IActionResult> Index(int? page)
        {
            var listTinTuc = InitParam.Db.TinTuc.AsNoTracking()
                           .Select(t => new TinTuc
                           {
                               Id = t.Id,
                               TieuDe = t.TieuDe,
                               GioiThieu = t.GioiThieu,
                               HinhAnhMinhHoa = t.HinhAnhMinhHoa,
                               NoiDung = t.NoiDung,
                               NgayTao=t.NgayTao,
                              
                           });

            
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfTinTuc = listTinTuc.ToPagedList(pageNumber, 2); // will only contain 25 products max because of the pageSize

            return View(onePageOfTinTuc);
        }

        // GET: TinTucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await InitParam.Db.TinTuc.AsNoTracking()
                .Include(t => t.FkLoaiTinNavigation)
                .Include(t => t.FkNgonNguNavigation)
                .Include(t => t.FkUserNguoiSuaNavigation)
                .Include(t => t.FkUserNguoiTaoNavigation)
                .Select(t => new TinTuc
                {
                }).FirstOrDefaultAsync();
            if (tinTuc == null)
            {
                return NotFound();
            }

            return View(tinTuc);
        }

        // GET: TinTucs/Create
        public IActionResult Create()
        {
            ViewData["FkLoaiTin"] = new SelectList(InitParam.Db.LoaiTin, "Id", "Id");
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id");
            ViewData["FkUserNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
            ViewData["FkUserNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName");
            return View();
        }

        // POST: TinTucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiTin,NgayTao,FkUserNguoiTao,NoiDung,Stt,LuotXem,Author,FkUserNguoiSua,NgaySua")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
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

        // GET: TinTucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinTuc = await InitParam.Db.TinTuc.FindAsync(id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            ViewData["FkLoaiTin"] = new SelectList(InitParam.Db.LoaiTin, "Id", "Id", tinTuc.FkLoaiTin);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", tinTuc.FkNgonNgu);
            ViewData["FkUserNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tinTuc.FkUserNguoiSua);
            ViewData["FkUserNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tinTuc.FkUserNguoiTao);
            return View(tinTuc);
        }

        // POST: TinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TieuDe,GioiThieu,HinhAnhMinhHoa,FkNgonNgu,FkLoaiTin,NgayTao,FkUserNguoiTao,NoiDung,Stt,LuotXem,Author,FkUserNguoiSua,NgaySua")] TinTuc tinTuc)
        {
            if (id != tinTuc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    InitParam.Db.Update(tinTuc);
                    await InitParam.Db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinTucExists(tinTuc.Id))
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
            ViewData["FkLoaiTin"] = new SelectList(InitParam.Db.LoaiTin, "Id", "Id", tinTuc.FkLoaiTin);
            ViewData["FkNgonNgu"] = new SelectList(InitParam.Db.NgonNgu, "Id", "Id", tinTuc.FkNgonNgu);
            ViewData["FkUserNguoiSua"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tinTuc.FkUserNguoiSua);
            ViewData["FkUserNguoiTao"] = new SelectList(InitParam.Db.User, "UserName", "UserName", tinTuc.FkUserNguoiTao);
            return View(tinTuc);
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
