using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts;
using App.Entities;
using App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly IRepository<Site> _sitesRepo;
        private readonly IMapper _mapper;

        public SitesController(IRepository<Site> sitesRepo, IMapper mapper)
        {
            _sitesRepo = sitesRepo;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Site>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var result = _sitesRepo.FindAll();

            if (result != null && result.Any())
            {
                return Ok(result);
            }

            // If sitesRepo do not exist in db then 204 will be returned
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Site), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] GetSiteByIdRequest request)
        {
            var result = _sitesRepo.FindByCondition((site => site.Id == request.Id));
            var firstOrDefault = result.FirstOrDefault();

            if (firstOrDefault != null)
            {
                return Ok(firstOrDefault);
            }

            // If site with specified Id do not exists in db then 404 will be returned.
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Site), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(AddSiteRequest request)
        {
            var site = _mapper.Map<Site>(request);

            await _sitesRepo.Create(site);

            return CreatedAtAction(nameof(GetById), new { id = site.Id }, site);
        }
    }
}
