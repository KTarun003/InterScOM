using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterScOM.Areas.Admin.Models;
using InterScOM.Areas.Staff.Controllers;
using InterScOM.Areas.Staff.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace InterScOM.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class AccountController : Controller
    {
        public AccountController(UserManager<AppUser> userManager)
        {
            _userMgr = userManager;
        }


        private readonly UserManager<AppUser> _userMgr;

        // GET: AccountController
        public ActionResult Index()
        {
            var users = _userMgr.Users.ToList();
            return View(users);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser([Bind("User,RoleName,Password")] UserVm userVm)
        {
            if (ModelState.IsValid)
            {
                userVm.User.EmailConfirmed = true;
                await _userMgr.CreateAsync(userVm.User, userVm.Password);
                await _userMgr.AddToRoleAsync(userVm.User, userVm.RoleName);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "admin,staff,parent")]
        // GET: AccountController/Edit/5
        public async Task<ActionResult> Edit(string email)
        {
            var user = await _userMgr.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "admin,staff,parent")]
        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("FirstName,LastName,PhoneNumber,Email")] AppUser user)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userMgr.FindByEmailAsync(user.Email);
                appUser.LastName = user.LastName;
                appUser.FirstName = user.FirstName;
                appUser.PhoneNumber = user.PhoneNumber;
                await _userMgr.UpdateAsync(appUser);
                if (await _userMgr.IsInRoleAsync(user,"admin"))
                {
                    return View(nameof(Index));
                }
                
                if (await _userMgr.IsInRoleAsync(user, "staff"))
                {
                    RedirectToAction("Dashboard", "Applications");
                }

                if (await _userMgr.IsInRoleAsync(user, "parent"))
                {
                    RedirectToAction("Index", "Home");
                }

            }
            return View(nameof(Edit),user);
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
