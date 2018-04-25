using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Command
{
    class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var employee = employeeService.ById(employeeId);

            StringBuilder result =new StringBuilder();

            result.Append($"ID: {employeeId} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}");

            return result.ToString();
        }
    }
}
