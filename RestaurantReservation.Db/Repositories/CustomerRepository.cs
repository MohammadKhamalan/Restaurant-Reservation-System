using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public CustomerRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersWithReservationsAbovePartySizeAsync(int MinPartySize)
        {
            var Customers = await _context.Customers
                .FromSql($"EXEC sp_GetCustomersWithLargePartySize {MinPartySize}")
                .ToListAsync();
            return Customers;
        }
    }
}
