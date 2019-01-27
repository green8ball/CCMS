using System;
using CCMS.Areas.Identity.Data;
using CCMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CCMS.Areas.Identity.IdentityHostingStartup))]
namespace CCMS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CCMSContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CCMSContextConnectionLocal")));

                services.AddDefaultIdentity<CCMSUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CCMSContext>();
                //services.AddIdentity<CCMSUser, IdentityRole>()
                //    .AddEntityFrameworkStores<CCMSContext>()
                //    .AddDefaultTokenProviders();
            });

        }
    }
}