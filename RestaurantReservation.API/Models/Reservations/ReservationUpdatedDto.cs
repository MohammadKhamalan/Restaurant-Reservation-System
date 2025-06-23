using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Models.Reservations
{
    public class ReservationUpdatedDto
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public int PartySize { get; set; }
    }
}
