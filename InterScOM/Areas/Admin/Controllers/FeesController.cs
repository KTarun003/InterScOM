using InterScOM.Areas.Admin.Models;
using InterScOM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Fees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fee.ToListAsync());
        }

        // GET: Admin/Fees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fee fee = await _context.Fee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // GET: Admin/Fees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentName,FeeStatus")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fee);
        }

        // GET: Admin/Fees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fee fee = await _context.Fee.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }
            return View(fee);
        }

        // POST: Admin/Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentName,FeeStatus")] Fee fee)
        {
            if (id != fee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        // GET: Admin/Fees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fee fee = await _context.Fee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // POST: Admin/Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Fee fee = await _context.Fee.FindAsync(id);
            _context.Fee.Remove(fee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeExists(int id)
        {
            return _context.Fee.Any(e => e.Id == id);
        }
    }
}
