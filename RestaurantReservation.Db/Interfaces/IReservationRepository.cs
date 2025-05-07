using RestaurantReservation.Db.Models.Entities;
using RestaurantReservation.Db.Models.Views;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IReservationRepository:IGenericRepository<Reservation>
    {
       
        Task<IEnumerable<Reservation>> GetReservationsByCustomer(int customerId);
        Task<IEnumerable<ReservationsDetails>> GetReservationsWithDetailsAsync();

    }
}
