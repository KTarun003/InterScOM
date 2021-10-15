using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.Staff.Controllers
{
    [Authorize(Roles = "staff")]
    [Area("Staff")]
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Fees
        public async Task<IActionResult> Index(int? id)
        {
            List<Fee> list = new List<Fee>();
            foreach (Fee item in _context.Fee)
            {
                if (item.FeeStatus.Equals("Due"))
                {
                    list.Add(item);
                }
            }
            foreach (Fee item in list)
            {
                item.Application = await _context.Application.FindAsync(item.ApplicationId);
            }
            if (id == null)
            {
                return View(list);
            }
            List<Fee> filteredList = new List<Fee>();
            foreach (Fee item in list)
            {
                if (item.ApplicationId == id)
                {
                    filteredList.Add(item);
                }
            }
            return View(filteredList);
        }

        // GET: Admin/Fees/Edit/5
        public async Task<IActionResult> Collect(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fee fee = await _context.Fee.FindAsync(id);
            fee.Application = await _context.Application.FindAsync(fee.ApplicationId);
            return View(fee);
        }

        // POST: Admin/Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Collect(int id, [Bind("Id,ParentName,FeeStatus,ApplicationId")] Fee fee)
        {
            if (id != fee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fee = await _context.Fee.FindAsync(id);
                    fee.FeeStatus = "Paid";
                    _context.Update(fee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeExists(fee.Id))
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
            return View(fee);
        }

        private bool FeeExists(int id)
        {
            return _context.Fee.Any(e => e.Id == id);
        }
    }
}
