﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class Configuration
    {
        public static string ConnectionString { get; set; } =
            "Server = (local)\\SQLEXPRESS;Database = Hospital;Integrated Security = True";
    }
}
