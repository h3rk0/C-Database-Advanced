using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Data
{
    public static class Configuration
    {
        public static string ConnectionString { get; set; } =
            "Server = (local)\\SQLEXPRESS;Database = BusTicketsSystem;Integrated Security = True";
    }
}
