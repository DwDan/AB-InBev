using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)") 
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500); 

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(p => p.Image)
                .HasMaxLength(255); 

            builder.OwnsOne(p => p.Rating, r =>
            {
                r.Property(rt => rt.Rate)
                    .HasColumnName("RatingRate")
                    .HasColumnType("decimal(5,2)") 
                    .IsRequired();

                r.Property(rt => rt.Count)
                    .HasColumnName("RatingCount")
                    .IsRequired();
            });
        }
    }
}
