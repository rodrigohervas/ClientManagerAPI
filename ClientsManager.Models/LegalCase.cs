using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Models
{
    public class LegalCase
    {
        public int Id { get; set; }

        //FK for Client
        public int Client_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal TrustFund { get; set; }


        //Prop for relationship with Client
        public Client Client { get; set; }

        //Prop for relationship with BillableActivity
        public ICollection<BillableActivity> BillableActivities { get; set; }
    }
}
