using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<List<Customer>> GetCustomersWithReservationsAbovePartySizeAsync(int MinPartySize);
    }
}
