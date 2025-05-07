using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Models.Views;
using RestaurantReservation.Db.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> ListManagersAsync();
        Task<IEnumerable<EmployeesWithRestaurantDetails>> GetEmployeesWithRestaurantsAsync();

    }
}
