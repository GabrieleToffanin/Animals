using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Logic
{
    public class AnimalsBusinessLogic : IMainBusinessLogic
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IMappingService _mapService;

        public AnimalsBusinessLogic(IAnimalRepository animalsRepo,
                                    IMappingService mapService)
        {
            _animalRepo = animalsRepo;
            _mapService = mapService;
        }
        public async Task<bool> Create(AnimalCreationRequest animal)
        {
            var currentAnimal = _mapService.MapFrom<AnimalCreationRequest, Animal>(animal);
            var result = await _animalRepo.Create(currentAnimal);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            return await _animalRepo.Delete(id);
        }

        public async IAsyncEnumerable<AnimalDTO> GetAllAnimals(string? search)
        {
            foreach(var animal in await _animalRepo
                .GetStartingWithAnimals(x => x!.Name!.ToLowerInvariant()
                .Contains(search!.ToLowerInvariant(), StringComparison.InvariantCulture)))
                    yield return _mapService.MapFrom<Animal, AnimalDTO>(animal);
        }

        //Actually leaving Animal.Id property into AnimalDTO escapes
        //the "problem" of providing the id via Controller Endpoint 
        //and leads to a cleaner implementation (idea! : obfuscate ID directly 
        //in the UWP.Client UI, having the ID in the models for the deserialization, that would give me 
        //a direct Bind to the Animal.Id provided by DB)
        public async Task<bool> Update(int id, AnimalUpdateRequest animal)
        {
            var currentAnimal = _mapService.MapFrom<AnimalUpdateRequest, Animal>(animal);

            var result = await Task.Run(async () => await _animalRepo.Delete(id))
                                             .ContinueWith(async (x) => await _animalRepo.Create(currentAnimal));


            return await result;
        }
    }
}
