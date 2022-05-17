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
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly IRepository _repo;
        private readonly IMappingService _mapService;
        //See comment at GetAllAnimals
        private List<AnimalDTO> _currentAnimals;
        public MainBusinessLogic(IRepository repo, IMappingService mapService)
        {
            _repo = repo;

            _mapService = mapService;
            _currentAnimals = new List<AnimalDTO>();
        }
        public async ValueTask<bool> Create(AnimalDTO animal)
        {
            var wantedData = _mapService.MapFrom<AnimalDTO, Animal>(animal);
            return await _repo.Create(wantedData);
        }

        public async ValueTask<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }

        public async ValueTask<IEnumerable<AnimalDTO>> GetAllAnimals()
        {

            foreach (var elem in await _repo.FetchAll())
                _currentAnimals.Add(_mapService.MapFrom<Animal, AnimalDTO>(elem));
                //Possibly doing :
                //yield return _mapService.MapFrom<Animal, AnimalDTO>(elem);
                //Would be better, allows to remove the _currentAnimals temp list.

            return _currentAnimals.Any() ? _currentAnimals : Enumerable.Empty<AnimalDTO>();
        }
        //Actually leaving Animal.Id property into AnimalDTO escapes
        //the "problem" of providing the id via Controller Endpoint 
        //and leads to a cleaner implementation (idea obfuscate ID directly 
        //in the UWP.Client UI, having the ID in the models for the deserialization, that would give me 
        //a direct Bind to the Animal.Id provided by DB)
        public async ValueTask<bool> Update(int id, AnimalDTO animal)
        {
            var toModifyAnimal = await _repo.GetById(id);
            if (toModifyAnimal is null)
                return false;


            toModifyAnimal = _mapService.MapFrom<AnimalDTO, Animal>(animal);
            return await _repo.Update(id, toModifyAnimal);
        }
    }
}
