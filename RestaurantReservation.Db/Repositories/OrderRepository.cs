using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public OrderRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }
       

        public async Task<double> CalculateAverageOrderAmount(int EmployeeId)
        {
            return await _context.Orders.Where(o => o.EmployeeId == EmployeeId).AverageAsync(t => t.TotalAmount);
        }


        public async Task<IEnumerable<Order>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
            return await _context.Orders.Where(re => re.ReservationId == reservationId).Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem).ToListAsync();
        }

        

        
    }
}
