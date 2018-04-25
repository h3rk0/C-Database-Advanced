using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public decimal Grade { get; set; }

        public int BusCompanyId { get; set; }
        public BusCompany BusCompany { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime DateAnDateTimeOfPublishing { get; set; }

    }
}
