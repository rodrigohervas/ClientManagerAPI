using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Models
{
    public class Client
    {
        //add props
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        
        //prop for relationship with Case
        public ICollection<LegalCase> LegalCases { get; set; }

        //TODO: add ICollection<Address> Addresses

        //TODO: add ICollection<Contact> Contacts
    }
}
