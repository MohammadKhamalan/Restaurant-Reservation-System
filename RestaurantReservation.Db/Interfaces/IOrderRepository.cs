using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Interfaces
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
       
        Task<double> CalculateAverageOrderAmount(int EmployeeId);
        Task<IEnumerable<Order>> ListOrdersAndMenuItemsAsync(int reservationId);
    }
}
