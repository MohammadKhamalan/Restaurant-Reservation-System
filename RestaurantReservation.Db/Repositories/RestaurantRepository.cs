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
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public RestaurantRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> fn_CalculateRestaurantRevenueAsync(int RestaurantId)
        {
            var total = await _context.Restaurants.Where(res => res.RestaurantId == RestaurantId).Select(f => _context.fn_CalculateRestaurantRevenue(f.RestaurantId)).FirstOrDefaultAsync();

            return total;
        }
    }
}
