using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Configurations
{
   public class EmployeesWithRestaurantDetailsConfigurations : IEntityTypeConfiguration<EmployeesWithRestaurantDetails>
    {
        public void Configure(EntityTypeBuilder<EmployeesWithRestaurantDetails> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_EmployeesWithRestaurants");
            builder.Property(e => e.EmployeeId).HasColumnName("employee_id");
            builder.Property(e => e.EmployeeFirstName).HasColumnName("First_Name");
            builder.Property(e => e.EmployeeLastName).HasColumnName("Last_Name");
            builder.Property(e => e.EmployeePosition).HasColumnName("Position");
        }
        }
}
