using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Models.Views;
using RestaurantReservation.Db.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomer(int customerId)
        {
            return await _context.Reservations.Include(r => r.Restaurant).Where(re => re.CustomerId == customerId).ToListAsync();
        }

        public async Task<IEnumerable<ReservationsDetails>> GetReservationsWithDetailsAsync()
        {
            return await _context.ReservationWithDetails.ToListAsync();
        }


    }
}