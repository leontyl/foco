using System;

namespace App.Models
{
    public class GetNextActionResponse : CheckInRequest
    {
        public Guid CaseNumber { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
