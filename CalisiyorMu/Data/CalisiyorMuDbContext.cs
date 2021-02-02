using CalisiyorMu.Models;
using Microsoft.EntityFrameworkCore;

namespace CalisiyorMu.Data
{
    public class CalisiyorMuDbContext : DbContext
    {
        public CalisiyorMuDbContext(DbContextOptions<CalisiyorMuDbContext> options) : base(options)
        {
        }



        public DbSet<Study> Studies { get; set; }
    }
}