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
    public class SpeciesBusinessLogic : ISpecieBusinessLogic
    {
        private readonly ISpecieRepository _specieRepository;
        private readonly IMappingService _mappingService;

        public SpeciesBusinessLogic(ISpecieRepository repository, IMappingService mappingService)
        {
            _specieRepository = repository;
            _mappingService = mappingService;
        }

        //No sense method, the creation of the specie related to the new Animal Entity should be
        //automatic.
        public async ValueTask CreateSpecieFromAnimal(AnimalDTO specieFromAnimal)
        {
            var currentSpecieFromAnimal = _mappingService.MapFrom<AnimalDTO, Specie>(specieFromAnimal);
            if (await _specieRepository.GetBySpecieName(currentSpecieFromAnimal.SpecieName) is null)
                await _specieRepository.Create(currentSpecieFromAnimal);
        }

        public async ValueTask<IQueryable<Specie>> FetchSpecies()
        {
            return await _specieRepository.FetchAll();
        }
    }
}
