using System.Threading.Tasks;
using App.Contracts;
using App.Entities;
using App.Exceptions.CheckInExceptions;
using App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/siteId/{siteId}/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICheckInService _checkInService;
        private readonly IMapper _mapper;

        public CustomersController(ICheckInService checkInService, IMapper mapper)
        {
            _checkInService = checkInService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckIn([FromRoute]SiteByIdRequest siteRequest, [FromBody] CheckInRequest checkInRequest)
        {
            var customer = _mapper.Map<Customer>(checkInRequest);

            try
            {
                var result = await _checkInService.CheckInAsync(siteRequest.SiteId, customer);

                if (result.IsExistent)
                {
                    return Conflict("Customer already performed checkin");
                }
                else
                {
                    return Ok(result.TicketNumber);
                }
            }
            catch (SiteDoesNotExistException)
            {
                return BadRequest("Specified site does not exists");
            }
        }
    }
}
