using System.Diagnostics;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login([Bind("AppUser,Password")] LogIn logIn)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                await _signInManager.SignOutAsync();
                AppUser user = await _userManager.FindByEmailAsync(logIn.AppUser.Email);
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, logIn.Password, logIn.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if (await _userManager.IsInRoleAsync(user, "admin"))
                    {
                        return RedirectToAction(nameof(Index), "AdminStats", new { area = "Admin" });
                    }
                    if (await _userManager.IsInRoleAsync(user, "staff"))
                    {
                        return RedirectToAction("Dashboard", "Applications", new { area = "Staff" });
                    }

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
