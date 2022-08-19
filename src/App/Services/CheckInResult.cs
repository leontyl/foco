using System;
using App.Entities;

namespace App.Services
{
    public class CheckInResult
    {
        public Guid TicketNumber { get; set; }
        public bool IsExistent { get; set; }
    }
}
