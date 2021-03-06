﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.DtoModels
{
    public class EmployeePersonalDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Address { get; set; }
    }
}
