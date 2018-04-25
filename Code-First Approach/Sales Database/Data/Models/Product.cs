﻿namespace P03_SalesDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class Product
    {
        public Product()
        {
            Sales=new List<Sale>();
        }

        public int  ProductId { get; set; }
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        //public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
