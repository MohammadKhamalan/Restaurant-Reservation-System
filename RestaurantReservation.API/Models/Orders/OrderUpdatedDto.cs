namespace RestaurantReservation.API.Models.Orders
{
    public class OrderUpdatedDto
    {
        public int ReservationId { get; set; }
        public int EmployeeId { get; set; }
        public int TotalAmount { get; set; }
    }
}
