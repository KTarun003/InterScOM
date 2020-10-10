using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Admin.Models;
using InterScOM.Data;

namespace InterScOM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VendorOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/VendorOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorOrders.ToListAsync());
        }

        // GET: Admin/VendorOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorOrders = await _context.VendorOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorOrders == null)
            {
                return NotFound();
            }

            return View(vendorOrders);
        }

        // GET: Admin/VendorOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/VendorOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VendorId,VendorName,OrderName,OrderRefNumber,OrderClass,DatePlaced,DateReceived,Quantity,Status")] VendorOrders vendorOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorOrders);
        }

        // GET: Admin/VendorOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorOrders = await _context.VendorOrders.FindAsync(id);
            if (vendorOrders == null)
            {
                return NotFound();
            }
            return View(vendorOrders);
        }

        // POST: Admin/VendorOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VendorId,VendorName,OrderName,OrderRefNumber,OrderClass,DatePlaced,DateReceived,Quantity,Status")] VendorOrders vendorOrders)
        {
            if (id != vendorOrders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorOrdersExists(vendorOrders.Id))
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
            return View(vendorOrders);
        }

        // GET: Admin/VendorOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorOrders = await _context.VendorOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorOrders == null)
            {
                return NotFound();
            }

            return View(vendorOrders);
        }

        // POST: Admin/VendorOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorOrders = await _context.VendorOrders.FindAsync(id);
            _context.VendorOrders.Remove(vendorOrders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorOrdersExists(int id)
        {
            return _context.VendorOrders.Any(e => e.Id == id);
        }
    }
}
