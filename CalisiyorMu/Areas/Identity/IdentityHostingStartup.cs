using CalisiyorMu.Areas.Identity;
using CalisiyorMu.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace CalisiyorMu.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
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