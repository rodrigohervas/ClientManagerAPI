using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Models
{
    public class BillableActivity
    {
        public int Id { get; set; }

        //FK for Case
        public int Case_Id { get; set; }

        //FK from Employee
        public int Employee_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime Start_DateTime { get; set; }

        public DateTime Finish_DateTime { get; set; }



        //prop for relationship with Employee
        public Employee Employee { get; set; }
    }
}
