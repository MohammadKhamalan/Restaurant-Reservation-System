using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IReservationRepository:IGenericRepository<Reservation>
    {
       
        Task<IEnumerable<Reservation>> GetReservationsByCustomer(int customerId);
    }
}
