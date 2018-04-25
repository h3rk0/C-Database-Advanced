using Employees.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Command
{
    class SetBirthDayCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetBirthDayCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            DateTime date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);

           var employeeName = employeeService.SetBirthDay(employeeId,date);

            return $"{employeeName}'s birthday was set to {args[1]}";
        }
    }
}
