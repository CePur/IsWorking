using CalisiyorMu.Areas.Identity.Data;
using CalisiyorMu.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalisiyorMu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<CalisiyorMuDbContext>();
                db.Database.Migrate();
                var db2 = scope.ServiceProvider.GetService<AuthContext>();
                db2.Database.Migrate();
            }

            host.Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}