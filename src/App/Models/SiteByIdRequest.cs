using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class SiteByIdRequest
    {
        [Required, Range(1, Int32.MaxValue)]
        public int SiteId { get; set; }
    }
}
