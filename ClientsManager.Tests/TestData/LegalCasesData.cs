using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.TestData
{
    public static class LegalCasesData
    {
        public static IEnumerable<LegalCase> getTestLegalCases()
        {
            return new List<LegalCase>
            {
                new LegalCase()
                {
                    Id = 1,
                    Client_Id = 1,
                    Title = "Title for Case 1",
                    Description = "Description for Case 1",
                    TrustFund = 1000.00m
                },
                new LegalCase()
                {
                    Id = 2,
                    Client_Id = 1,
                    Title = "Title for Case 2",
                    Description = "Description for Case 2",
                    TrustFund = 2000.00m
                },
                new LegalCase()
                {
                    Id = 3,
                    Client_Id = 2,
                    Title = "Title for Case 3",
                    Description = "Description for Case 3",
                    TrustFund = 3000.00m
                },
                new LegalCase()
                {
                    Id = 4,
                    Client_Id = 2,
                    Title = "Title for Case 4",
                    Description = "Description for Case 4",
                    TrustFund = 4000.00m
                }
            };
        }
            
    }
}
