using Animals.Core.Interfaces;
using Animals.Core.Models;
using Animals.Core.Models.DTOInputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animals.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeciesInfoController : ControllerBase
    {
        private readonly ISpecieBusinessLogic _context;
        public SpeciesInfoController(ISpecieBusinessLogic context)
        {
            _context = context;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IAsyncEnumerable<SpecieDTO>>> Index()
        {
            return Ok(_context.FetchSpecies());
        }

        [HttpGet("/GetAnimalsBySpecieName/{specieName}")]
        public async ValueTask<ActionResult<IAsyncEnumerable<SpecieDTO>>> GetAnimalsBySpecieName(string specieName)
        {
            return Ok(_context.FetchSpeciesWithFilter((x => x.SpecieName == specieName)));
        }
    }
}
