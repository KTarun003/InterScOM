﻿using InterScOM.Areas.Admin.Models;
using InterScOM.Areas.Forum.Models;
using InterScOM.Areas.Staff.Models;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<VendorOrders> VendorOrders { get; set; }

        public DbSet<Vendor> Vendor { get; set; }
    }
}
