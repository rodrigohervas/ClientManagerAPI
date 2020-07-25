using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Tests.TestData
{
    public static class LegalCasesData
    {
        public static IEnumerable<LegalCase> GetLegalCases()
        {
            return new List<LegalCase>
            {
                new LegalCase()
                {
                    Id = 1,
                    Client_Id = 1,
                    Title = "Title for Case 1",
                    Description = "Description for Case 1",
                    TrustFund = 1000.00m, 
                    BillableActivities = new List<BillableActivity> 
                    {
                        new BillableActivity()
                        {
                            Id = 1,
                            LegalCase_Id = 1,
                            Title = "Title of BA 1 of case 1",
                            Description = "Description of BA 1 of case 1",
                            Price = 100.00m,
                            Start_DateTime = new DateTime(2020, 06, 21, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 21, 16, 00, 00)
                        },
                        new BillableActivity()
                        {
                            Id = 2,
                            LegalCase_Id = 1,
                            Title = "Title of BA 2 of case 1",
                            Description = "Description of BA 2 of case 1",
                            Price = 200.00m,
                            Start_DateTime = new DateTime(2020, 06, 22, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 22, 16, 00, 00)
                        },
                        new BillableActivity()
                        {
                            Id = 3,
                            LegalCase_Id = 1,
                            Title = "Title of BA 3 of case 1",
                            Description = "Description of BA 3 of case 1",
                            Price = 300.00m,
                            Start_DateTime = new DateTime(2020, 06, 23, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 23, 16, 00, 00)
                        }
                    }
                },
                new LegalCase()
                {
                    Id = 2,
                    Client_Id = 1,
                    Title = "Title for Case 2",
                    Description = "Description for Case 2",
                    TrustFund = 2000.00m,
                    BillableActivities = new List<BillableActivity>
                    {
                        new BillableActivity()
                        {
                            Id = 1,
                            LegalCase_Id = 1,
                            Title = "Title of BA 1 of case 1",
                            Description = "Description of BA 1 of case 1",
                            Price = 100.00m,
                            Start_DateTime = new DateTime(2020, 06, 21, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 21, 16, 00, 00)
                        },
                        new BillableActivity()
                        {
                            Id = 2,
                            LegalCase_Id = 1,
                            Title = "Title of BA 2 of case 1",
                            Description = "Description of BA 2 of case 1",
                            Price = 200.00m,
                            Start_DateTime = new DateTime(2020, 06, 22, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 22, 16, 00, 00)
                        }
                    }
                },
                new LegalCase()
                {
                    Id = 3,
                    Client_Id = 2,
                    Title = "Title for Case 3",
                    Description = "Description for Case 3",
                    TrustFund = 3000.00m,
                    BillableActivities = new List<BillableActivity>
                    {
                        new BillableActivity()
                        {
                            Id = 1,
                            LegalCase_Id = 1,
                            Title = "Title of BA 1 of case 1",
                            Description = "Description of BA 1 of case 1",
                            Price = 100.00m,
                            Start_DateTime = new DateTime(2020, 06, 21, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 21, 16, 00, 00)
                        },
                        new BillableActivity()
                        {
                            Id = 2,
                            LegalCase_Id = 1,
                            Title = "Title of BA 2 of case 1",
                            Description = "Description of BA 2 of case 1",
                            Price = 200.00m,
                            Start_DateTime = new DateTime(2020, 06, 22, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 22, 16, 00, 00)
                        },
                        new BillableActivity()
                        {
                            Id = 3,
                            LegalCase_Id = 1,
                            Title = "Title of BA 3 of case 1",
                            Description = "Description of BA 3 of case 1",
                            Price = 300.00m,
                            Start_DateTime = new DateTime(2020, 06, 23, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 23, 16, 00, 00)
                        }
                    }
                },
                new LegalCase()
                {
                    Id = 4,
                    Client_Id = 2,
                    Title = "Title for Case 4",
                    Description = "Description for Case 4",
                    TrustFund = 4000.00m,
                    BillableActivities = new List<BillableActivity>
                    {
                        new BillableActivity()
                        {
                            Id = 1,
                            LegalCase_Id = 1,
                            Title = "Title of BA 1 of case 1",
                            Description = "Description of BA 1 of case 1",
                            Price = 100.00m,
                            Start_DateTime = new DateTime(2020, 06, 21, 9, 30, 00),
                            Finish_DateTime = new DateTime(2020, 06, 21, 16, 00, 00)
                        }
                    }
                }
            };
        }
            
    }
}
