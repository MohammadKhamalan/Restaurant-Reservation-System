using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Models.OrderItems
{
    public class OrderItemCreationDto
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
       
    }
}
