using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.ViewModels
{
    public class UserVm
    {
        public AppUser User { get; set; }

        public string RoleName { get; set; }

        public string Password { get; set; }
    }
}
