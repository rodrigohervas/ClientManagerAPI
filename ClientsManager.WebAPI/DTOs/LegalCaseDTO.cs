using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class LegalCaseDTO
    {
        public int Id { get; set; }

        public int Client_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal TrustFund { get; set; }
    }
}
