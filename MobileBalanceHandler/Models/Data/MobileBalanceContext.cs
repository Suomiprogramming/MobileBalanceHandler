using Microsoft.EntityFrameworkCore;

namespace MobileBalanceHandler.Models.Data
{
    public class MobileBalanceContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public MobileBalanceContext(DbContextOptions<MobileBalanceContext> options) : base(options)
        {
            
        }
    }
}