using AutoMapper;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.AutoMapperProfiles
{
    public class AutoMapperProfiles: Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDTO>();

            CreateMap<TimeFrame, TimeFrameDTO>();

            CreateMap<Employee, EmployeeWithTimeFramesDTO>();

            CreateMap<TimeFrameDTO, TimeFrame>();
        }
    }
}
