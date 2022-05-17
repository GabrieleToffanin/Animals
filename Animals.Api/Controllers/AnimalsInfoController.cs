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
        private readonly ISpecieBusinessLogic _species;

        public AnimalsInfoController(IMainBusinessLogic animals, ISpecieBusinessLogic species)
        {
            _animals = animals;
            _species = species;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<AnimalDTO>>> FetchAll()
        {
            return Ok(await _animals.GetAllAnimals());
        }

        [HttpPost("/CreateAnimal")]
        public async ValueTask<IActionResult> Create([FromBody]AnimalDTO animal)
        {
            await _species.CreateSpecieFromAnimal(animal);
            var result = await _animals.Create(animal);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("/DeleteAnimal")]
        public async ValueTask<IActionResult> Delete([FromBody]int Id)
        {
            var result = await _animals.Delete(Id);

            return result ? Ok() : BadRequest();
        }
        
        //ToDo, improve the logic, may be a good idea leaving the Animal.Id prop into the DTO for cleaner implementation ?
        [HttpPost("/UpdateAnimal")]
        public async ValueTask<IActionResult> Update([FromBody]Animal updatedContet)
        {
            return await _animals.Update(updatedContet) ? Ok() : BadRequest();
        }

        
    }
}
