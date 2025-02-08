using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("int").ValueGeneratedOnAdd();

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.Status)
            .HasConversion<string>() 
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>() 
            .HasMaxLength(20);

        builder.OwnsOne(p => p.Name, r =>
        {
            r.Property(rt => rt.Firstname)
             .IsRequired()
             .HasMaxLength(50);

            r.Property(rt => rt.Lastname)
             .IsRequired()
             .HasMaxLength(50);
        });

        builder.OwnsOne(p => p.Address, r =>
        {
            r.Property(rt => rt.City)
             .IsRequired()
             .HasMaxLength(50);

            r.Property(rt => rt.Street)
             .IsRequired()
             .HasMaxLength(150);

            r.Property(rt => rt.Number)
             .IsRequired();

            r.Property(rt => rt.Zipcode)
             .IsRequired()
             .HasMaxLength(20);

            r.OwnsOne(rt => rt.Geolocation, g =>
            {
                g.Property(rt => rt.Latitude)
                 .HasMaxLength(50)
                 .IsRequired(false);

                g.Property(rt => rt.Longitude)
                 .HasMaxLength(50)
                 .IsRequired(false);
            });
        });
    }
}
