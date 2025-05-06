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
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public MenuItemRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> ListOrderedMenuItems(int ReservationId)
        {
            return await _context.OrderItems
                .Include(o => o.Order)
                .Include(i => i.MenuItem)
                .Where(o => o.Order.OrderId == ReservationId).Select(m => m.MenuItem).ToListAsync();
        }
    }
}
