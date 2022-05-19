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
        public async Task<ActionResult<IAsyncEnumerable<Animal>>> FetchAll()
        {
            return Ok(_animals.GetAllAnimals());
        }

        [HttpPost("CreateAnimal")]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody]AnimalDTO animal)
        {
            
            var result = await _animals.Create(animal);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("Animal/{id?}")]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var result = await _animals.Delete(id);

            return result ? Ok() : BadRequest();
        }
        
        //ToDo, improve the logic, may be a good idea leaving the Animal.Id prop into the DTO for cleaner implementation ?
        [HttpPut("UpdateAnimal")]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromBody]AnimalDTO updatedContet)
        {
            await _animals.Delete(updatedContet.Id);
            await _animals.Create(updatedContet);

            return Ok();
        }

        
    }
}
