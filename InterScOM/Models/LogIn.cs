using InterScOM.Areas.Admin.Models;

namespace InterScOM.Models
{
    public class LogIn
    {
        public AppUser AppUser { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
