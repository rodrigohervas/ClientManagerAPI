using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class ContactDTO
    {
        public int Id { get; set; }

        //FK from Client
        public int Client_Id { get; set; }

        //FK from Address
        public int Address_Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        //nvarchar(15)
        public string Telephone { get; set; }

        //nvarchar(15)
        public string Cellphone { get; set; }

        //nvarchar(320)
        public string Email { get; set; }
    }
}
