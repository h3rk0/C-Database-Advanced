namespace P03_SalesDatabase.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class Configuration
    {
        public static string ConnectionString { get; set; } =
            "Server = (local)\\SQLEXPRESS;Database = SalesDatabase4;Integrated Security = True;";
    }
}
