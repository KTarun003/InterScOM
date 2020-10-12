using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterScOM.Areas.Admin.Models;

namespace InterScOM.Areas.Admin
{
    public class UserVm
    {
        public AppUser User { get; set; }

        public string RoleName { get; set; }

        public string Password { get; set; }
    }
}
