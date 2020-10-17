using Microsoft.AspNetCore.Identity;

namespace InterScOM.Areas.Admin.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AppId { get; set; }
    }
}
