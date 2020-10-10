using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InterScOM.Areas.Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InterScOM.Areas.Admin.Models;
using InterScOM.Areas.Staff.Models;

namespace InterScOM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Query> Queries { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Supplies> Supplies { get; set; }

        public DbSet<Fee> Fee { get; set; }

        public DbSet<Application> Application { get; set; }

        public DbSet<InterScOM.Areas.Admin.Models.Fee> Fee { get; set; }
        public DbSet<InterScOM.Areas.Admin.Models.VendorOrders> VendorOrders { get; set; }
        public DbSet<InterScOM.Areas.Admin.Models.Vendor> Vendor { get; set; }
    }
}
