using System;
using Microsoft.EntityFrameworkCore;

namespace App.Entities
{
    [Index(nameof(Queue.SiteId), Name = "Index_SiteId")] // search will be performed by SiteId field
    public class Queue
    {
        public int Id { get; set; } // PK
        public bool IsCompleted { get; set; }
        public Guid TicketNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string CustomerId { get; set; } // FK
        public Customer Customer { get; set; }
        public int SiteId { get; set; } // FK
        public Site Site { get; set; }
    }
}
