using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace App.Models
{
    public class GetSiteByIdRequest
    {
        [FromRoute, Required, Range(1, Int32.MaxValue)]
        public int Id { get; set; }
    }
}
