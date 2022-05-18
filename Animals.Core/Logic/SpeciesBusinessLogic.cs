﻿using Animals.Core.Interfaces;
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
        public async IAsyncEnumerable<SpecieDTO> FetchSpecies()
        {
            foreach (var item in await _specieRepository.FetchAll())
                yield return _mappingService.MapFrom<Specie, SpecieDTO>(item);
        }

        public async IAsyncEnumerable<SpecieDTO> FetchSpeciesWithFilter(Func<Specie, bool> filter)
        {
            foreach (var item in await _specieRepository.GetAnimalsFromSpecieName(filter))
                yield return _mappingService.MapFrom<Specie, SpecieDTO>(item);
                
        }
    }
}
