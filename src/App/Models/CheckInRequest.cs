using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class CheckInRequest
    {
        [Required, MaxLength(50)]
        public string PersonalId { get; set; }
        [Required, MaxLength(12)]
        public string PhoneNumber { get; set; }
        [Required, Range(typeof(DateTime), "1900-01-01", "2000-12-31")]
        public DateTime DateOfBirth { get; set; }
        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
    }
}
