using ClientsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class ContactWithAddressDTO
    {
        public int Id { get; set; }

        public int Client_Id { get; set; }

        public int Address_Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Telephone { get; set; }

        public string Cellphone { get; set; }

        public string Email { get; set; }

        public AddressDTO Address { get; set; }
    }
}
