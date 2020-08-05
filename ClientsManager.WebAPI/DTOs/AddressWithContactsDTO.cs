using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class AddressWithContactsDTO
    {
        public int Id { get; set; }

        public int Client_Id { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public ICollection<ContactDTO> Contacts { get; set; }
    }
}
