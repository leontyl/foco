using System;

namespace App.Entities
{
    public class Customer
    {
        public int Id { get; set; } // PK
        public string PhoneNumber;
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
