using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrdersBase.Models;

namespace OrderEfRepository
{
    public sealed class OrderContext : DbContext
    {
        private static readonly string SchemaName = "orders_service";
        private static bool _isMigrated;
        private readonly IConfiguration _configuration;

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public OrderContext(IConfiguration configuration)
        {
            this._configuration = configuration;

            if (!_isMigrated)
            {
                this.Database.Migrate();
                _isMigrated = true;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration["DbConnectingString"], x => x.MigrationsHistoryTable("__EFMigrationsHistory", SchemaName));
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