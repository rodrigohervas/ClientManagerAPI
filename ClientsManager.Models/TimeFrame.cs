using System;
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
    public class TimeFrame: IEntity
    {
        public int Id { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "The Employee is mandatory")]
        //[Range(1.0, Double.PositiveInfinity, ErrorMessage = "The Employee Id is Mandatory!")]
        public int Employee_Id { get; set; }

        //[Required(ErrorMessage = "The Title is mandatory")]
        public string Title { get; set; }

        //[Required(ErrorMessage = "The Description is mandatory")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "The Price is mandatory")]
        //[Range(0.1, Double.PositiveInfinity, ConvertValueInInvariantCulture = false, ErrorMessage = "The price must be over 0.0")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "The Start Time is mandatory")]
        //[DataType(DataType.DateTime, ErrorMessage = "The Start Time must be a valid Date and Time")]
        public DateTime Start_DateTime { get; set; }

        //[Required(ErrorMessage = "The Finish Time is mandatory")]
        //[DataType(DataType.DateTime, ErrorMessage = "The Start Time must be a valid Date and Time")]
        public DateTime Finish_DateTime { get; set; }
    }
}
