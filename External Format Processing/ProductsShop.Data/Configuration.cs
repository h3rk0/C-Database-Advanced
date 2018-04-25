using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsShop.Data
{
    public class Configuration
    {
        public static string ConnectionString =>
            "Server = (local)\\SQLEXPRESS;Database = ProductsShop;Integrated Security = True";

    }
}
