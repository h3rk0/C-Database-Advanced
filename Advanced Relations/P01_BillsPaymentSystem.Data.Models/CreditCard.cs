namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public decimal Limit { get;  set; } //set setter to private for exercise 4

        public decimal MoneyOwed { get;  set; } //set setter to private for exercise 4

        public decimal LimitLeft => Limit - MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            this.MoneyOwed -= amount;
        }

    }
}
