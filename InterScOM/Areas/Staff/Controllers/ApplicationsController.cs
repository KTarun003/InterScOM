using InterScOM.Areas.Admin.Models;
using InterScOM.Areas.Staff.Models;
using InterScOM.Data;
using InterScOMML.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InterScOM.Areas.Staff.Controllers
{
    //[Authorize(Roles = "staff")]
    [Area("Staff")]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<AppUser> _userMgr;
        public ApplicationsController(ApplicationDbContext context, UserManager<AppUser> userMgr)
        {
            _context = context;
            _userMgr = userMgr;
        }

        public async Task<IActionResult> Dashboard()
        {
            System.Collections.Generic.List<Application> applications = await _context.Application.ToListAsync();
            Dashboard dashboard = new Dashboard
            {
                Applications = applications.Count()
            };
            foreach (Application application in applications)
            {
                if (application.Status.Equals("Accepted"))
                {
                    dashboard.Accepted++;
                }
                else if (application.Status.Equals("Rejected"))
                {
                    dashboard.Rejected++;
                }
                else if (application.Status.Equals("Pending") && (dashboard.PendingApplications.Count <= 5))
                {
                    dashboard.PendingApplications.Add(application);
                }
            }
            return View(dashboard);
        }

        // GET: Staff/Applications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Application.ToListAsync());
        }

        // GET: Staff/Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application application = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Staff/Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FathersName,MothersName,Dob,Percentage,AnnualIncome,InternetRating,Status,Email")] Application application)
        {
            if (ModelState.IsValid)
            {
                application.ApplicationDate = DateTime.Now;
                application.Status = "Pending";
                ModelInput input = new ModelInput
                {
                    StudentId = application.Id,
                    CGPA = application.Percentage
                };
                ModelOutput output = ConsumeModel.Predict(input);
                int fees = (int)output.Score;
                fees = (int)Math.Floor(output.Score / 1000);
                application.Fees = fees * 1000;
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(application);
        }

        // GET: Staff/Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Staff/Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FathersName,MothersName,Dob,Percentage,AnnualIncome,InternetRating")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Dashboard));
            }
            return View(application);
        }

        // GET: Staff/Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application application = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Staff/Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Application application = await _context.Application.FindAsync(id);
            _context.Application.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }

        // GET: Staff/Applications/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application application = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Staff/Applications/Approve/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveConfirmed(int id)
        {
            Application application = await _context.Application.FindAsync(id);
            try
            {
                application.Status = "Accepted";
                _context.Update(application);
                AppUser parent = new AppUser
                {
                    FirstName = application.FathersName,
                    UserName = application.Name,
                    AppId = application.Id,
                    Email = application.Email,
                    EmailConfirmed = true
                };
                Fee fee = new Fee
                {
                    ParentName = application.FathersName,
                    FeeStatus = "Due"
                };
                await _context.Fee.AddAsync(fee);
                await _context.SaveChangesAsync();
                await _userMgr.CreateAsync(parent, "School@123");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(application.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Dashboard));

        }

        // GET: Staff/Applications/Approve/5
        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application application = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Staff/Applications/Reject/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectConfirmed(int id)
        {
            Application application = await _context.Application.FindAsync(id);
            try
            {
                application.Status = "Rejected";
                _context.Update(application);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(application.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Dashboard));

        }

        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.Id == id);
        }
    }
}
