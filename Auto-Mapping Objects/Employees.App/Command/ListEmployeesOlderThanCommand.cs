using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Command
{
    class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public ListEmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
            var age = int.Parse(args[0]);

            var employees = employeeService.ListEmployeesOlderThan(age);

            StringBuilder result = new StringBuilder();

            foreach (var e in employees)
            {
                var manager = "[no manager]";

                if (e.managerFirstName != null && e.managerLastName != null)
                {
                    manager = $"{e.managerFirstName} {e.managerLastName}";
                }

                result.AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:f2} - Manager: {manager}");
            }

            return result.ToString();
        }
    }
}
