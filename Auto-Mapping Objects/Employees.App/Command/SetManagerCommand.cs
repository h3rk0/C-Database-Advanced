using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Command
{
    public class SetManagerCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetManagerCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerId = int.Parse(args[1]);

            var result = employeeService.SetManager(employeeId, managerId);

            return result;
        }
    }
}
