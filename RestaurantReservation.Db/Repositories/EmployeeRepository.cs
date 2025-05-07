using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public EmployeeRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeesWithRestaurantDetails>> GetEmployeesWithRestaurantsAsync()
        {
          
            return await _context.EmployeesWithRestaurants.ToListAsync();
        

        }

        public async Task<IEnumerable<Employee>> ListManagersAsync()
        {
            return await _context.Employees.Where(e => e.Position == "Manager").ToListAsync();
        }
    }
}
