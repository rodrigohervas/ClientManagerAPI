using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }

        public int Client_Id { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}
