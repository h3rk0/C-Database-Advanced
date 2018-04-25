using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data
{
    internal class Configuration
    {
        public static string ConnectionString { get;} =
            "Server = (local)\\SQLEXPRESS;Database = FootballBetting;Integrated Security = True;";

    }
}
