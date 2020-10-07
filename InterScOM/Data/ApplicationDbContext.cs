using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InterScOM.Areas.Forum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    }
}
