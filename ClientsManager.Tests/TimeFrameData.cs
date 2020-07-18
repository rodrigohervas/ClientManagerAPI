﻿using ClientsManager.Models;
using ClientsManager.WebAPI.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Tests
{
    public static class TimeFrameData
    { 
        public static IEnumerable<TimeFrame> GetTestTimeFrames()
        {
            return new List<TimeFrame> {
                new TimeFrame
                {
                    Id = 1,
                    Employee_Id = 1,
                    Title = "timeframe 1",
                    Description = "this is the timeframe 1",
                    Price = 120.50m,
                    Start_DateTime = new DateTime(2020, 06, 20, 9, 30, 00),
                    Finish_DateTime = new DateTime(2020, 06, 20, 16, 00, 00)
                },
                new TimeFrame
                {
                    Id = 2,
                    Employee_Id = 1,
                    Title = "timeframe 2",
                    Description = "this is the timeframe 2",
                    Price = 400m,
                    Start_DateTime = new DateTime(2020, 06, 21, 10, 00, 00),
                    Finish_DateTime = new DateTime(2020, 06, 21, 17, 30, 00)
                },
                new TimeFrame
                {
                    Id = 3,
                    Employee_Id = 2,
                    Title = "timeframe 3",
                    Description = "this is the timeframe 3",
                    Price = 300.50m,
                    Start_DateTime = new DateTime(2020, 06, 23, 8, 15, 00),
                    Finish_DateTime = new DateTime(2020, 06, 23, 13, 30, 00)
                }
            };
        }
    }
}
