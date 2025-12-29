using FCG.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Games.Infra.Mappers;

public class OrderItemMap : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

        builder.HasOne(o => o.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(o => o.Game)
            .WithMany()
            .HasForeignKey(o => o.GameId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property(o => o.CreateDate)
                .IsRequired();
    }
}
