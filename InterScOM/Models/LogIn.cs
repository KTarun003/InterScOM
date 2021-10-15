using Web.Areas.Admin.Models;

namespace Web.Models
{
    public class LogIn
    {
        public AppUser AppUser { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
