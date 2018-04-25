﻿using System.Collections;
using System.Collections.Generic;

namespace BusTicketsSystem.Models
{
    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<BusStation> BusStations { get; set; }
    }
}