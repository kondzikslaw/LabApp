using LabApp.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabApp.DataAccess.Data
{
    public class LabAppDbContext : DbContext
    {
        public LabAppDbContext(DbContextOptions<LabAppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=LabAppStorage;Integrated Security=True;Trust Server Certificate=True");
        }

    }
}
