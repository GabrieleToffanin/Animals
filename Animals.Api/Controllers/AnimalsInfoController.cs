using Animals.Core.Interfaces;
using Animals.Core.Models;
using Animals.Core.Models.DTOInputModels;
using Animals.EF.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsInfoController : ControllerBase
    {
        private readonly IMainBusinessLogic _animals;
        public AnimalsInfoController(IMainBusinessLogic animals)
        {
            _animals = animals;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> FetchAll()
        {
            return Ok(await _animals.GetAllAnimals());
        }

        [HttpPost("/Create")]
        public async ValueTask<IActionResult> Create(AnimalDTO animal)
        {
            var result = await _animals.Create(animal);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("/Delete")]
        public async ValueTask<IActionResult> Delete(int Id)
        {
            var result = await _animals.Delete(Id);

            return result ? Ok() : BadRequest();
        }
    }
}
