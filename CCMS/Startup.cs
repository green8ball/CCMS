using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using CCMS.Utils;
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireWFMRole", policy => policy.RequireRole("WFM", "Admin"));
                options.AddPolicy("RequireHRRole", policy => policy.RequireRole("Human Resources"));
            });

            
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
                    Context.Departments.Add(new Department
                    {
                        Name = "Temp",
                        Description = "Temporary Department for newly added employees"
                    });
                    Context.SaveChanges();

                    Department newDepartment = new Department
                    {
                        Name = "DHA",
                        Description = "Primary Deparment for Dish"
                    };

                    Context.Departments.Add(newDepartment);
                    
                    Context.SaveChanges();
                    
                    foreach (DateTime day in Helper.EachDay(DateTime.Parse("2018-01-01"), DateTime.Parse("2020-12-31")))
                    {
                        int allowedVal = 0;
                        switch (day.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                allowedVal = 56;
                                break;
                            case DayOfWeek.Tuesday:
                                allowedVal = 56;
                                break;
                            case DayOfWeek.Wednesday:
                                allowedVal = 48;
                                break;
                            case DayOfWeek.Thursday:
                                allowedVal = 40;
                                break;
                            case DayOfWeek.Friday:
                                allowedVal = 32;
                                break;
                            case DayOfWeek.Saturday:
                                allowedVal = 24;
                                break;
                            case DayOfWeek.Sunday:
                                allowedVal = 24;
                                break;
                        }
                        Allotment newAllotment = new Allotment
                        {
                            Date = day,
                            Allowed = allowedVal,
                            Department = newDepartment
                        };
                        Context.Allotments.Add(newAllotment);
                        //Context.SaveChanges();
                    }
                    Context.SaveChanges();

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

                
                foreach (int year in Helper.EachYear(newEmployee.HireDate.Year, newEmployee.HireDate.Year + 5))
                {
                    int pto = 0;
                    switch (year - newEmployee.HireDate.Year)
                    {
                        case 0:
                            pto = 32;
                            break;
                        case 1:
                            pto = 80;
                            break;
                        default:
                            pto = 120;
                            break;
                    }

                    TimeOffAllowed newTimeOffAllowed = new TimeOffAllowed
                    {
                        Employee = newEmployee,
                        Year = year,
                        UTO = 0,
                        PTO = pto
                    };

                    Context.TimeOffAlloweds.Add(newTimeOffAllowed);
                    Context.SaveChanges();

                }
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
