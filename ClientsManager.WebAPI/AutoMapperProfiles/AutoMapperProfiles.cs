﻿using AutoMapper;
using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.AutoMapperProfiles
{
    /// <summary>
    /// Class for Automapper DI configuration
    /// </summary>
    public class AutoMapperProfiles: Profile
    {

        /// <summary>
        /// Method to hold Automapper DI mappings
        /// </summary>
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeDTO>();

            CreateMap<TimeFrame, TimeFrameDTO>();

            CreateMap<Employee, EmployeeWithTimeFramesDTO>();

            CreateMap<TimeFrameDTO, TimeFrame>();

            CreateMap<EmployeeType, EmployeeTypeDTO>();
        }
    }
}
