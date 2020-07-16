﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ClientsManager.Models
{
    /// <summary>
    /// TimeFrame entity
    /// </summary>
    public class TimeFrame
    {
        public int Id { get; set; }

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
