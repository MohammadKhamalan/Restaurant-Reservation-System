using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.RestaurantId);
            builder.Property(r => r.RestaurantId).HasColumnName("restaurant_id");
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Address).IsRequired().HasMaxLength(100);
            builder.Property(r => r.PhoneNumber).IsRequired().HasColumnName("phone_number").HasMaxLength(13);
            builder.Property(r => r.OpenHours).HasColumnName("opening_hours").IsRequired();

            builder.HasMany(r => r.Reservations)
               .WithOne(res => res.Restaurant)
               .HasForeignKey(res => res.RestaurantId);

            builder.HasMany(r => r.Employees)
                   .WithOne(e => e.Restaurant)
                   .HasForeignKey(e => e.RestaurantId);
            builder.HasMany(r => r.Tables)
                   .WithOne(t => t.Restaurant)
                   .HasForeignKey(t => t.RestaurantId);

            builder.HasMany(r => r.MenuItems)
                   .WithOne(m => m.Restaurant)
                   .HasForeignKey(m => m.RestaurantId);


        }
    }
}
