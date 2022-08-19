using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CustomersController : ControllerBase
    {
    }
}
