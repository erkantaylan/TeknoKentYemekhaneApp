using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YemekhaneApp.Persistence.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Önce environment variable'dan al, yoksa default kullan
            var connectionString = "ConnectionStrings__YemekhaneDb";

            optionsBuilder.UseSqlServer(connectionString); // MSSQL için UseSqlServer

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}