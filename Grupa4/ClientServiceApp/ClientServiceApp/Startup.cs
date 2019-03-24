using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClientServiceApp.Data;
using ClientServiceApp.Models;
using ClientServiceApp.Services;

namespace ClientServiceApp
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
            services.AddDbContext<ProductsAndServicesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MorfeuszConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MorfeuszConnection")));

            services.AddDbContext<SalaryIncreaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MorfeuszConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();



            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            AddRole(serviceProvider).Wait();
        }

        private async Task AddRole(IServiceProvider serviceProvider)
        {
            //Dodajemy swoje Role
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Employee", "Client" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            //Stwórzmy Admina
            var admin = new ApplicationUser
            {
                //UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                UserName = "Admin",
                //Email = Configuration.GetSection("UserSettings")["UserEmail"]
                Email = "admin@admin.pl"
            };

            //string UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            string UserPassword = "Qazwsx789*";
            var _user = await UserManager.FindByEmailAsync("admin@admin.pl");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(admin, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //dodajemy naszego admina do roli "Admin"
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }

        }
    }
}
