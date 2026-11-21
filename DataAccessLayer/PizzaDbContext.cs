
using Microsoft.EntityFrameworkCore;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для Entity Framework Core.
    /// Содержит наборы сущностей пицц и скидок.
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
        /// Создаёт контекст базы данных.
        /// </summary>
        public PizzaDbContext()
        {
        }

        /// <summary>
        /// Создаёт контекст с заданными параметрами подключения.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста.</param>
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Настраивает строку подключения к LocalDB,
        /// если параметры подключения не были переданы извне.
        /// </summary>
        /// <param name="optionsBuilder">Построитель параметров контекста.</param>
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        /// <summary>
        /// Настраивает сопоставление моделей с таблицами базы данных.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели данных.</param>
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
