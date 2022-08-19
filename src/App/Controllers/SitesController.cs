using System.Collections.Generic;
using System.Linq;
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
    [Route("v{version:apiVersion}/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly IRepository<Site, int> _sitesRepo;
        private readonly IActionService _actionService;
        private readonly IMapper _mapper;

        public SitesController(IRepository<Site, int> sitesRepo, IMapper mapper, IActionService actionService)
        {
            _sitesRepo = sitesRepo;
            _mapper = mapper;
            _actionService = actionService;
        }

        /// <summary>
        /// Getting all sites
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Getting site by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{siteId}")]
        [ProducesResponseType(typeof(Site), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] SiteByIdRequest request)
        {
            var result = _sitesRepo.FindByCondition((site => site.Id == request.SiteId));
            var firstOrDefault = result.FirstOrDefault();

            if (firstOrDefault != null)
            {
                return Ok(firstOrDefault);
            }

            // If site with specified Id do not exists in db then 404 will be returned.
            return NotFound();
        }

        /// <summary>
        /// Adding new site
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Site), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody]AddSiteRequest request)
        {
            var site = _mapper.Map<Site>(request);

            await _sitesRepo.Create(site);

            return CreatedAtAction(nameof(GetById), new { siteId = site.Id }, site);
        }


        [HttpGet]
        [Route("{siteId}/actions/callNext")]
        [ProducesResponseType(typeof(GetNextActionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNexSiteAction([FromRoute] SiteByIdRequest request)
        {
            try
            {
                var queue = await _actionService.GetNext(request.SiteId);

                if (queue == null)
                {
                    // Nothing not found for site
                    return NoContent();
                }

                var result = _mapper.Map<GetNextActionResponse>(queue);

                return Ok(result);

            }
            catch (SiteDoesNotExistException)
            {
                return BadRequest("Specified site does not exists");
            }
        }
    }
}
