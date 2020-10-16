﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Admin.Models;
using InterScOM.Data;
using Microsoft.AspNetCore.Authorization;

namespace InterScOM.Areas.Staff.Controllers
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
        public IActionResult Index(int? id)
        {
            var list = from l in _context.Fee
                    where l.FeeStatus.Equals("Due")
                    select l;
            if (id == null)
            {
                return View(list);
            }

            list = from l in list
                where l.ApplicationId == id
                   select l;

            return View(list);
        }

        // GET: Admin/Fees/Edit/5
        public async Task<IActionResult> Collect(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fee = await _context.Fee.FindAsync(id);
            fee.Application = await _context.Application.FindAsync(fee.ApplicationId);
            return View(fee);
        }

        // POST: Admin/Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Collect(int id, [Bind("Id,ParentName,FeeStatus")] Fee fee)
        {
            if (id != fee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
