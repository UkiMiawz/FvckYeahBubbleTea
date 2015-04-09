﻿using System;

namespace ThickClientManager.DataTransferObject
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }    
        }

        public DateTime DateOfBirth { get; set; }
    }
}