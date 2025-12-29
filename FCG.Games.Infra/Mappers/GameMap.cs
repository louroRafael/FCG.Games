using FCG.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Games.Infra.Mappers;

public class GameMap : IEntityTypeConfiguration<GameEntity>
{
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.ToTable("Games");
            
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
                .ValueGeneratedNever()
                .IsRequired();

        builder.Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(g => g.Description)
                .IsRequired()
                .HasMaxLength(1000);

        builder.Property(g => g.Genre)
                .IsRequired();

        builder.Property(g => g.Platform)
                .IsRequired();

        builder.Property(g => g.Developer)
                .IsRequired()
                .HasMaxLength(50);

        builder.Property(g => g.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        builder.Property(u => u.CreateDate)
                .IsRequired();
    }
}
