using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Data
{
    public class Configuration
    {
        public static string ConnectionString =>
            $"Server = (local)\\SQLEXPRESS;Database = Employees;Integrated Security = True;";
    }
}
