using LabApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabApp.Data
{
    public class LabAppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Test> Tests => Set<Test>();

        public DbSet<Client> Clients => Set<Client>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
