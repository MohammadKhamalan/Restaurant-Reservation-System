using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);
            builder.Property(e=>e.EmployeeId).HasColumnName("employee_id");
            builder.Property(e => e.FirstName).IsRequired().HasColumnName("first_name").HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasColumnName("last_name").HasMaxLength(50);
            builder.Property(e => e.Position).IsRequired().HasColumnName("position").HasMaxLength(10);
            builder.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            builder.HasOne(r => r.Restaurant)
              .WithMany(e => e.Employees)
              .HasForeignKey(r => r.RestaurantId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(o => o.Orders)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId);

        }
        }
}
