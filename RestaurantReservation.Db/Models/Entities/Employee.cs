﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models.Entities
{
   public class Employee
    {
        public int EmployeeId { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
