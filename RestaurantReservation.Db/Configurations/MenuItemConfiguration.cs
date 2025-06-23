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
    class MenuItemConfiguration :IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {

            builder.HasKey(m => m.ItemId);
            builder.Property(m => m.ItemId).HasColumnName("item_id");
            builder.Property(m => m.RestaurantId).HasColumnName("restaurant_id");
            builder.Property(m => m.Description).IsRequired().HasColumnName("description").HasMaxLength(200);
            builder.Property(m => m.Name).IsRequired().HasColumnName("name").HasMaxLength(50);
            builder.Property(m => m.Price).IsRequired().HasColumnName("price").HasColumnType("decimal(10, 2)");

            builder.HasOne(r => r.Restaurant)
            .WithMany(m => m.MenuItems)
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(oi => oi.OrderItems)
                .WithOne(m => m.MenuItem)
                .HasForeignKey(oi => oi.ItemId);
        }
        }
}
