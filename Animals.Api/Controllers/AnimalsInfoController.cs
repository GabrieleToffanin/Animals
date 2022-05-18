using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using Animals.EF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Animals.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsInfoController : ControllerBase
    {
        private readonly IMainBusinessLogic _animals;

        public AnimalsInfoController(IMainBusinessLogic animals)
        {
            _animals = animals;
        }

        [HttpGet]
        
        public async ValueTask<ActionResult<IAsyncEnumerable<Animal>>> FetchAll()
        {
            return Ok(_animals.GetAllAnimals());
        }

        [HttpPost("/CreateAnimal")]
        //[Authorize(Roles = "Administrator")]
        public async ValueTask<IActionResult> Create([FromBody]AnimalDTO animal)
        {
            //await _species.CreateSpecieFromAnimal(animal);
            var result = await _animals.Create(animal);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("/DeleteAnimal/{id?}")]
        [Authorize(Roles = "Administrator")]
        public async ValueTask<IActionResult> Delete(int id)
        {
            
            var result = await _animals.Delete(id);

            return result ? Ok() : BadRequest();
        }
        
        //ToDo, improve the logic, may be a good idea leaving the Animal.Id prop into the DTO for cleaner implementation ?
        [HttpPost("/UpdateAnimal")]
        [Authorize(Roles = "Administrator")]
        public async ValueTask<IActionResult> Update([FromBody]AnimalDTO updatedContet)
        {
            await _animals.Delete(updatedContet.Id);
            await _animals.Create(updatedContet);

            return Ok();
        }

        
    }
}
