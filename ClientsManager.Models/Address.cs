using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Models
{
    public class Address
    {
        public int Id { get; set; }

        //FK from Client
        public int Client_Id { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }



        //Navigational property for relationship with Client
        public Client Client { get; set; }

        //Navigational property for relationship with Contact
        public ICollection<Contact> Contacts { get; set; }
    }
}
