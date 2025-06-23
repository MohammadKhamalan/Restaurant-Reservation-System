using RestaurantReservation.Db.Models.Entities;

namespace RestaurantReservation.API.Models.MenuItems
{
    public class MenuItemUpdatedDto
    {
        
        public int RestaurantId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
