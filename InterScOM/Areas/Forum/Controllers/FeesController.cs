using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Admin.Models;
using InterScOM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace InterScOM.Areas.Forum.Controllers
{
    [Authorize(Roles = "parent"), Area("Forum")]
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeesController(ApplicationDbContext context, UserManager<AppUser> userMgr)
        {
            _context = context;
            _userMgr = userMgr;
        }

        private readonly UserManager<AppUser> _userMgr;

        // GET: Admin/Fees
        public async Task<IActionResult> Index(string name)
        {
            AppUser user = await _userMgr.FindByNameAsync(name);
            List<Fee> list = await _context.Fee.ToListAsync();
            foreach (var item in list)
            {
                if (item.ApplicationId == user.AppId)
                {
                    if (item.FeeStatus.Equals("Due"))
                    {
                        item.Application = await _context.Application.FindAsync(item.ApplicationId);
                        return View(item);
                    }
                }
                
            }

            return NotFound();
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
                return RedirectToAction("Index","Home",new {area=""});
            }
            return View(nameof(Index),fee);
        }

        private bool FeeExists(int id)
        {
            return _context.Fee.Any(e => e.Id == id);
        }
    }
}
