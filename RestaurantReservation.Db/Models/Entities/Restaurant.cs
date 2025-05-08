using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models.Entities
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OpenHours { get; set; }
        public ICollection<Table> Tables { get; set; } = new List<Table>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    }
}
