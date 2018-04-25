using System;
using System.Collections.Generic;
using System.Linq;
using Employees.DtoModels;
using Employees.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Employees.Services
{
    using AutoMapper;
    using Employees.Data;
    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);

            context.Employees.Add(employee);

            context.SaveChanges();

        }

        public string SetBirthDay(int employeeId,DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            employee.BirthDay = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeeDto;
        }

        public ManagerDto ManagerById(int managerId)
        {
            var manager = context.Employees.Include(e => e.ManagerEmployees)
                .SingleOrDefault(m => m.Id == managerId);

            var managerDto = Mapper.Map<ManagerDto>(manager);

            return managerDto;
        }

        public string SetManager(int employeeId, int managerId)
        {
            var employee = context.Employees.Find(employeeId);

            var manager = context.Employees.Find(managerId);

            employee.ManagerId = managerId;

            context.SaveChanges();

            return
                $"Employee {employee.FirstName} {employee.LastName}'s manager is set to Manager {manager.FirstName} {manager.LastName}";
        }

        public ICollection<EmployeeManagerDto> ListEmployeesOlderThan(int age)
        {
            var employees = context.Employees.Where(e => DateTime.Now.Year - e.BirthDay.Value.Year > age)
                .ToArray();

            var employeesDto = Mapper.Map<ICollection<EmployeeManagerDto>>(employees);

            return employeesDto;
        }
    }
}
