using FCG.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Games.Infra.Mappers;

public class PromotionMap : IEntityTypeConfiguration<PromotionEntity>
{
    public void Configure(EntityTypeBuilder<PromotionEntity> builder)
    {
        builder.ToTable("Promotions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .IsRequired();

        builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(p => p.PercentualDiscount)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

        builder.Property(p => p.StartDate)
                .IsRequired();

        builder.Property(p => p.EndDate)
                .IsRequired();

        builder.Property(p => p.GameId)
            .IsRequired();

        builder.HasOne(p => p.Game)
                .WithMany()
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.CreateDate)
                .IsRequired();
    }
}