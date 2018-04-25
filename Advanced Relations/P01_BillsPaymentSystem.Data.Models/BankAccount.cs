namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; } //set setter to private for exercise 4

        public string BankName { get; set; }

        public string Swift { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

    }
}
