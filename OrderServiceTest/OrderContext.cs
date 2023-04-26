using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderServiceTest.Models;

namespace OrderServiceTest
{
    public class OrderContext: DbContext
    {
        private static string SchemaName = "orders_service";
        private static bool IsMigrated;
        
        private IConfiguration configuration;
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public OrderContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            
            if (!IsMigrated)
            {
                var migrations = this.Database.GetPendingMigrations();

                this.Database.Migrate();
                IsMigrated = true;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(configuration["DbConnectingString"], x => x.MigrationsHistoryTable("__EFMigrationsHistory", SchemaName));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);

           var builder = modelBuilder.Entity<Order>();
           builder.HasMany(p => p.Lines)
               .WithOne(x => x.Order)
               .HasForeignKey(x => x.OrderId);

           modelBuilder.Entity<OrderLine>().Property(x => x.Id).IsRequired().ValueGeneratedNever();

        }
    }
}