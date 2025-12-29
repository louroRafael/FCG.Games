using FCG.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Games.Infra.Mappers;

public class OrderMap : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

        builder.Property(o => o.OrderNumber)
                .HasMaxLength(20);

        builder.Property(o => o.Status)
                .IsRequired();

        builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        builder.Property(o => o.PaymentMethod)
                .IsRequired();

        builder.Property(o => o.CreateDate)
                .IsRequired();
    }
}