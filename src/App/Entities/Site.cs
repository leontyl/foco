using System.Collections.Generic;

namespace App.Entities
{
    public class Site
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public IEnumerable<Customer> Customers {get; set; }
    }
}
