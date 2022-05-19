using Animals.Core.Interfaces;
using Animals.Core.Models;
using Animals.Core.Models.DTOInputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpecieBusinessLogic _context;
        public SpeciesController(ISpecieBusinessLogic context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IAsyncEnumerable<SpecieDTO>>> Index([FromQuery]string? specieName)
        {
            return specieName != null 
                ? Ok(_context.FetchSpeciesWithFilter(x => x.SpecieName.ToLowerInvariant() == specieName.ToLowerInvariant()))
                : Ok(_context.FetchSpecies());
        }
    }
}
