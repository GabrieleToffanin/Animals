using Animals.Core.Interfaces;
using Animals.Core.Models;
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
        private readonly ISpecieRepository _specieRepository;
        private readonly IMappingService _mapService;



        //Actual Problem, Creating and Auto-Update of One To Many relationship
        //with Specie(One) and Many(Animal), no clue atm.

        
        public AnimalsBusinessLogic(IAnimalRepository animalsRepo,
            ISpecieRepository speciesRepo, 
            IMappingService mapService)
        {
            _specieRepository = speciesRepo;
            _animalRepo = animalsRepo;
            _mapService = mapService;
        }
        public async ValueTask<bool> Create(AnimalDTO animal)
        {
            var currentAnimal = _mapService.MapFrom<AnimalDTO, Animal>(animal);
            var result = await _animalRepo.Create(currentAnimal);
            return result;
        }

        public async ValueTask<bool> Delete(int id)
        {
            return await _animalRepo.Delete(id);
        }

        public async ValueTask<IQueryable<Animal>> GetAllAnimals()
        {
            
            return await _animalRepo.FetchAll();
        }
        //Actually leaving Animal.Id property into AnimalDTO escapes
        //the "problem" of providing the id via Controller Endpoint 
        //and leads to a cleaner implementation (idea! : obfuscate ID directly 
        //in the UWP.Client UI, having the ID in the models for the deserialization, that would give me 
        //a direct Bind to the Animal.Id provided by DB)
        public async ValueTask<bool> Update(Animal animal)
        {
            return await _animalRepo.Update(animal);
        }
    }
}
