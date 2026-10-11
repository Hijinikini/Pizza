using Microsoft.EntityFrameworkCore;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст БД для Entity Framework Core.
    /// Содержит DbSet пицц и скидок.
    /// </summary>
    public class PizzaDbContext : DbContext
    {
        /// <summary>
        /// Таблица пицц.
        /// </summary>
        public DbSet<Pizza> Pizzas { get; set; }

        /// <summary>
        /// Таблица правил скидок.
        /// </summary>
        public DbSet<SizeDiscount> SizeDiscounts { get; set; }

        /// <summary>
        /// Настраивает строку подключения к LocalDB.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        /// <summary>
        /// Настраивает маппинг сущностей на таблицы БД.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
            modelBuilder.Entity<Pizza>().HasKey(p => p.Id);
            modelBuilder.Entity<Pizza>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<SizeDiscount>().ToTable("SizeDiscounts");
            modelBuilder.Entity<SizeDiscount>().HasKey(d => d.Id);
            modelBuilder.Entity<SizeDiscount>()
                .HasIndex(d => d.Size)
                .IsUnique();
        }
    }
}