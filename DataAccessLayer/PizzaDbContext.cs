using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class PizzaDbContext<T> : DbContext
        where T : class, IDomainObject
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>();
        }
    }
}