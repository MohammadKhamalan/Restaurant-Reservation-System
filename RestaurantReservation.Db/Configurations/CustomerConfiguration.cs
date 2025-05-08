using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RestaurantReservation.Db.Models.Entities;
namespace RestaurantReservation.Db.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);
            builder.Property(c => c.CustomerId).HasColumnName("customer_id");
            builder.Property(c => c.FirstName).IsRequired().HasColumnName("First_Name").HasMaxLength(100);
            builder.Property(c => c.LastName).IsRequired().HasColumnName("Last_Name").HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasColumnName("Email").HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).IsRequired().HasColumnName("Phone_Number").HasMaxLength(13);
            builder.HasMany(c => c.Reservations)
                  .WithOne(r => r.Customer)
                  .HasForeignKey(r => r.CustomerId);

        }
    }
}
