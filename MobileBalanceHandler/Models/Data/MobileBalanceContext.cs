using Microsoft.EntityFrameworkCore;
using MobileBalanceHandler.Models.MapConfigurations;

namespace MobileBalanceHandler.Models.Data
{
    public class MobileBalanceContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public MobileBalanceContext(DbContextOptions<MobileBalanceContext> options) : base(options)
        {
            
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PaymentDbMap());
        }
    }
}