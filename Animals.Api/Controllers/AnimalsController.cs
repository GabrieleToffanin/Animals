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
    public class AnimalsController : ControllerBase
    {
        private readonly IMainBusinessLogic _animals;

        public AnimalsController(IMainBusinessLogic animals)
        {
            _animals = animals;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AnimalDTO>>> FetchAll([FromQuery] string? search)
        {

            if (!ModelState.IsValid)
                return BadRequest("Json object has wrong properties");


            return Ok(_animals.GetAllAnimals(search ?? ""));

            
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Creator")]
        public async Task<IActionResult> Create([FromBody]AnimalCreationRequest animal)
        {
            
            var result = await _animals.Create(animal);

            return result ? Ok() : BadRequest();
        }

        
        [HttpDelete("{id?}")]
        [Authorize(Roles = "Administrator, Creator")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            
            var result = await _animals.Delete(id);

            return result ? Ok() : BadRequest();
        }
        
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Creator")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody]AnimalUpdateRequest updatedContet)
        {
            await _animals.Update(id, updatedContet);

            return Ok();
        }

        
    }
}
