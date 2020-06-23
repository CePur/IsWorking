using System;
using CalisiyorMu.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CalisiyorMu.Areas.Identity.IdentityHostingStartup))]
namespace CalisiyorMu.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CalisiyorMuDb")));

                services.AddDefaultIdentity<AppUser>()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<AuthContext>();
            });
        }
    }
}