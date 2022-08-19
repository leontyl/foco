using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace App.Entities
{
    [Index(nameof(Customer.SiteId), Name = "Index_SiteId")]
    public class Customer
    {
        public string Id { get; set; } // PK
        public string PhoneNumber;
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SiteId { get; set; } // FK
        public Site Site { get; set; }
        public Queue Queue { get; set; }
    }
}
