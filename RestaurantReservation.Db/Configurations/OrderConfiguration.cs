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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId).HasColumnName("order_id");
            builder.Property(o => o.ReservationId).HasColumnName("reservation_id");
            builder.Property(o => o.EmployeeId).HasColumnName("employee_id");
            builder.Property(o => o.OrderDate).IsRequired().HasColumnName("order_date");
            builder.Property(o => o.TotalAmount).IsRequired().HasColumnName("total_amount");

            builder.HasOne(o => o.Reservation)
              .WithMany(r => r.Orders)
              .HasForeignKey(o => o.ReservationId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(o => o.Employee)
             .WithMany(e => e.Orders)
             .HasForeignKey(o => o.EmployeeId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OrderItems)
              .WithOne(oi => oi.Order)
              .HasForeignKey(oi => oi.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
        }
        }
}
