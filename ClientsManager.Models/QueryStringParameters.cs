using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientsManager.Models
{
    /// <summary>
    /// Class that holds all the parameters necessary for Paging and Sorting 
    /// </summary>
    public class QueryStringParameters
    {
        //Page number requested. Defaults to 1
        public int pageNumber { get; set; } = 1;

        //Maximum results allowed per page. Defaults to 15.
        private int _maxPageSize { get; set; } = 15;

        //Results per page requested. Defaults to 10.
        private int _pageSize { get; set; } = 10;
        public int pageSize
        {
            get { return _pageSize; }
            set 
            {
                //the requested Page Size can't exceed the Max Page Size allowed
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            }
        }
    }
}
