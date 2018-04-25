using System;
using System.Collections;
using System.Collections.Generic;

namespace BusTicketsSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<Review> Reviews { get; set; }    

    }
}