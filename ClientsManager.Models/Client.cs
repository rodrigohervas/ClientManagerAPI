using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }


        //Navigational property for relationship with LegalCase
        public ICollection<LegalCase> LegalCases { get; set; }

        //Navigational property for relationship with Contact
        public ICollection<Contact> Contacts { get; set; }

        //Navigational property for relationship with Address
        public ICollection<Address> Addresses { get; set; }
    }
}
