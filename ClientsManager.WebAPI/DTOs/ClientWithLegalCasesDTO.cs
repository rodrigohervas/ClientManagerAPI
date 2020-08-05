using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsManager.WebAPI.DTOs
{
    public class ClientWithLegalCasesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public ICollection<LegalCaseDTO> LegalCases { get; set; }
    }
}
