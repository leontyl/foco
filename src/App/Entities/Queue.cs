using System;
using Microsoft.EntityFrameworkCore;

namespace App.Entities
{
    [Index(nameof(Queue.SiteId), Name = "Index_SiteId")]
    public class Queue
    {
        public int Id { get; set; } // PK

        public int SiteId { get; set; } // FK
        public Site Site { get; set; }

        public int CustomerId { get; set; } // FK
        public Customer Customer { get; set; }

        public Guid TicketNumber { get; set; }
    }
}
