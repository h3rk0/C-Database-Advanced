using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Employees.DtoModels;
using Employees.Models;

namespace Employees.App
{
    class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeePersonalDto>();
            CreateMap<Employee, ManagerDto>();
            CreateMap<Employee, EmployeeManagerDto>();
        }
    }
}
