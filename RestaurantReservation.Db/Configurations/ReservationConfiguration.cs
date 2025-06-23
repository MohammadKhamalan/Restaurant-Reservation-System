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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.ReservationId).HasColumnName("reservation_id");
            builder.Property(r => r.CustomerId).HasColumnName("customer_id");
            builder.Property(r => r.RestaurantId).HasColumnName("restaurant_id");
            builder.Property(r => r.TableId).HasColumnName("table_id");
            builder.Property(r => r.PartySize).IsRequired().HasColumnName("party_size");
            builder.Property(r => r.ReservationDate).IsRequired().HasColumnName("reservation_date");

            builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Restaurant)
                   .WithMany(res => res.Reservations)
                   .HasForeignKey(r => r.RestaurantId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(r => r.Table)
                   .WithMany(t => t.Reservations)
                   .HasForeignKey(r => r.TableId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(r => r.Orders)
                   .WithOne(o => o.Reservation)
                   .HasForeignKey(o => o.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
        }
}
