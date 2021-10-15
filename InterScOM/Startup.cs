using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Web.Areas.Admin.Data;
using Web.Areas.Admin.Models;
using Web.Data;

namespace Web
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
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<IdentityContext>();
            services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(builder.ConnectionString));
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole",
                    policy => policy.RequireRole("admin"));
                options.AddPolicy("RequireStaffRole",
                    policy => policy.RequireRole("staff"));
                options.AddPolicy("RequireParentRole",
                    policy => policy.RequireRole("parent"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            SeedData(serviceProvider).Wait();
            
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
            RoleManager<AppRole> roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
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
            await userManager.AddToRoleAsync(adminUser, "admin");
            await userManager.AddToRoleAsync(staffUser, "staff");
            await userManager.AddToRoleAsync(parentUser, "parent");
        }
    }
}
