using AutoMapper;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.AutoMapperProfiles
{
    public class EmployeeProfile: Profile
    {

        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
