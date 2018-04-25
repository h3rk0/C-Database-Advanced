using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Command
{
    class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public ManagerInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var managerId = int.Parse(args[0]);

            var manager = employeeService.ManagerById(managerId);

            StringBuilder result=new StringBuilder();

            result.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.ManagerEmployeesCount}");

            foreach (var e in manager.ManagerEmployees)
            {
                result.AppendLine($"    - {e.FirstName} {e.LastName} - ${e.Salary:f2}");
            }

            return result.ToString();
        }
    }
}
