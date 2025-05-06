using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public TableRepository(RestaurantReservationDbContext context) : base(context)
        {
            _context = context;
        }
       
    }
}
