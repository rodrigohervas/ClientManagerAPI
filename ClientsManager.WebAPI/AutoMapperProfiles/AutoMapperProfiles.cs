using AutoMapper;
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

            CreateMap<BillableActivity, BillableActivityDTO>();

            CreateMap<Employee, EmployeeWithBillableActivitiesDTO>();

            CreateMap<BillableActivityDTO, BillableActivity>();

            CreateMap<EmployeeType, EmployeeTypeDTO>();

            CreateMap<LegalCase, LegalCaseDTO>();

            CreateMap<LegalCase, LegalCaseWithBillableActivitiesDTO>();

            CreateMap<Contact, ContactDTO>();

            CreateMap<Contact, ContactWithAddressDTO>();

            CreateMap<Address, AddressDTO>();

            CreateMap<Address, AddressWithContactsDTO>();

            CreateMap<Client, ClientDTO>();

            CreateMap<Client, ClientWithLegalCasesDTO>();

            CreateMap<Client, ClientWithAddressesDTO>();

            CreateMap<Client, ClientWithContactsDTO>();
        }
    }
}
