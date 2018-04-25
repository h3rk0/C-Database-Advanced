using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Command
{
    class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var employee = employeeService.PersonalById(employeeId);

            StringBuilder result = new StringBuilder();

            string birthDay = "[No birthday specified]";

            if (employee.BirthDay != null)
            {
                birthDay = employee.BirthDay.Value.ToString("dd-MM-yyyy");
            }
            
            string address = employee.Address ?? "[no address specified]";

            result.Append($"ID: {employeeId} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}"
                          + Environment.NewLine);

            result.Append($"Birthday: {birthDay}"
                          + Environment.NewLine);

            result.Append($"Address: {address}");

            return result.ToString();
        }
    }
}
