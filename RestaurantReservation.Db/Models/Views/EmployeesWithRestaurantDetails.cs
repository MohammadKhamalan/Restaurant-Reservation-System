﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Models.Views
{
    public class EmployeesWithRestaurantDetails
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string RestaurantName { get; set; }
        public string EmployeePosition { get; set; }
       
    }
}
