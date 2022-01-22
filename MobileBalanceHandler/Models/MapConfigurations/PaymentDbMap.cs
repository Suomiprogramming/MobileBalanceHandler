using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MobileBalanceHandler.Models.MapConfigurations
{
    public class PaymentDbMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasMaxLength(36);
            builder.Property(p => p.Sum).HasColumnType("numeric(7,2)");
            builder.Property(p => p.PhoneNumber).HasMaxLength(12);
            builder.Property(p => p.PaymentDate).HasDefaultValue(DateTime.Now);
        }
    }
}