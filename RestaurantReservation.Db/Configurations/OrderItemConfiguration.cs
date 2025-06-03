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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.OrderItemId);
            builder.Property(i => i.OrderItemId).HasColumnName("order_item_id");
            builder.Property(i => i.OrderId).HasColumnName("order_id");
            builder.Property(i => i.ItemId).HasColumnName("item_id");
            builder.Property(i => i.Quantity).HasColumnName("quantity");
            builder.HasOne(o => o.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(o => o.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(m => m.MenuItem)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(mi => mi.ItemId)
                     .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
