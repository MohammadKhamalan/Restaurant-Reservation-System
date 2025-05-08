using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models.Entities
{
    public class Table
    {
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int Capacity { get; set; }
        public ICollection<Reservation> Reservations = new List<Reservation>();
    }
}
