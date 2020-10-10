using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterScOM.Areas.Admin.Data;
using InterScOM.Areas.Admin.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using InterScOM.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterScOM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AwsRds")));
            services.AddIdentity<AppUser, AppRole>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<IdentityContext>();
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AwsRds")));
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                if (roleManager.FindByNameAsync("admin") == null &&
                    userManager.FindByEmailAsync("Test@admin.com") == null)
                {
                    SeedData(serviceProvider).Wait();
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private async Task SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            AppUser adminUser = new AppUser
            {
                FirstName = "Test",
                LastName = "Admin",
                UserName = "Admin",
                Email = "admin@Test.com"
            };
            AppUser staffUser = new AppUser
            {
                FirstName = "Test",
                LastName = "Staff",
                UserName = "Staff",
                Email = "staff@Test.com"
            };
            AppUser parentUser = new AppUser
            {
                FirstName = "Test",
                LastName = "Parent",
                UserName = "Parent",
                Email = "parent@Test.com"
            };
            await userManager.CreateAsync(adminUser, "Test@admin1");
            await userManager.CreateAsync(staffUser, "Test@staff1");
            await userManager.CreateAsync(parentUser, "Test@parent1");

            AppRole adminRole = new AppRole()
            {
                Name = "admin"
            };
            AppRole staffRole = new AppRole()
            {
                Name = "staff"
            };
            AppRole parentRole = new AppRole()
            {
                Name = "parent"
            };
            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(staffRole);
            await roleManager.CreateAsync(parentRole);
        }
    }
}
