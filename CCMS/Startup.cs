using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CCMS
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)

        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<CCMSUser>>();
            var Context = serviceProvider.GetRequiredService<CCMSContext>();

            string[] roleNames = { "Admin", "WFM", "Staff", "Manager", "Sales Leader", "Human Resources" };
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

            if (Context.Employees.ToList().Count == 0)
            {
                if (Context.Departments.ToList().Count() == 0)
                {
                    Department newDepartment = new Department
                    {
                        Name = "DHA",
                        Description = "Primary Deparment for Dish"
                    };

                    Context.Departments.Add(newDepartment);
                    Context.SaveChanges();
                    
                    foreach (DateTime day in EachDay(DateTime.Parse("2019-01-01"), DateTime.Parse("2019-12-31")))
                    {
                        Allotment allotment = new Allotment
                        {
                            Date = day,
                            Allowed = 40
                        };
                        Context.Allotments.Add(allotment);
                        Context.SaveChanges();

                        DepartmentAllotment departmentAllotment = new DepartmentAllotment
                        {
                            DepartmentID = newDepartment.Id,
                            AllotmentID = allotment.Id
                        };
                        Context.DepartmentAllotments.Add(departmentAllotment);
                        await Context.SaveChangesAsync();
                    }

                }

                Employee newEmployee = new Employee
                {
                    FirstName = "Joshua",
                    MiddleName = "Ryan",
                    LastName = "Ortmann",
                    HireDate = DateTime.Parse("2018-08-27"),
                    Department = Context.Departments.Single(d => d.Name == "DHA")
                };

                Context.Employees.Add(newEmployee);
                Context.SaveChanges();

                CCMSUser newUser = new CCMSUser
                {
                    UserName = newEmployee.Id.ToString(),
                    Employee = newEmployee
                };

                var result = await UserManager.CreateAsync(newUser, "Temp1234!");

                if (result.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(newUser, "Admin");
                }
            }
        }
    }
}
