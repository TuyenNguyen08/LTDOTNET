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
    public class GioKhamsController : Controller
    {
        private readonly NBenhVien7CContext _context;

        public GioKhamsController(NBenhVien7CContext context)
        {
            _context = context;
        }

        // GET: GioKhams
        public async Task<IActionResult> Index()
        {
            return View(await _context.GioKham.ToListAsync());
        }
        public async Task<IActionResult> GioKham_Read([DataSourceRequest]DataSourceRequest request)
        {
            var listGioKham = _context.GioKham.AsNoTracking()
                .Select(t => new GioKhamViewModel
                {
                    Id = t.Id,
                    Gio=t.Gio
                    
                });
            var result = await listGioKham.ToDataSourceResultAsync(request);
            return Json(result);
        }
        // GET: GioKhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioKham = await _context.GioKham
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioKham == null)
            {
                return NotFound();
            }

            return View(gioKham);
        }

        // GET: GioKhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GioKhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gio")] GioKham gioKham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioKham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gioKham);
        }

        // GET: GioKhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioKham = await _context.GioKham.FindAsync(id);
            if (gioKham == null)
            {
                return NotFound();
            }
            return View(gioKham);
        }

        // POST: GioKhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gio")] GioKham gioKham)
        {
            if (id != gioKham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioKham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioKhamExists(gioKham.Id))
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
            return View(gioKham);
        }

        // GET: GioKhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gioKham = await _context.GioKham
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gioKham == null)
            {
                return NotFound();
            }

            return View(gioKham);
        }

        // POST: GioKhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gioKham = await _context.GioKham.FindAsync(id);
            _context.GioKham.Remove(gioKham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioKhamExists(int id)
        {
            return _context.GioKham.Any(e => e.Id == id);
        }
    }
}
