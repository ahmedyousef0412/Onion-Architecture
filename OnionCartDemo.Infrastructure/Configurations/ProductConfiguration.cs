using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionCartDemo.Domain.Entities;

namespace OnionCartDemo.Infrastructure.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);  

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        builder.Property(p => p.ImageUrl)
            .IsRequired()
            .HasMaxLength(200)
        .HasColumnName("ImageUrl");
    }
}
