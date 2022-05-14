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
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly IRepository _repo;
        private readonly IMappingService _mapService;
        public MainBusinessLogic(IRepository repo, IMappingService mapService)
        {
            _repo = repo;
            _mapService = mapService;
        }
        public async ValueTask<bool> Create(AnimalDTO animal)
        {
            var wantedData = _mapService.MapFrom<AnimalDTO, Animal>(animal);
            if (await _repo.Create(wantedData))
                return true;

            return false;
        }

        public async ValueTask<bool> Delete(int id)
        {
            if (await _repo.Delete(id))
                return true;

            return false;

        }

        public async ValueTask<IEnumerable<Animal>> GetAllAnimals()
        {
            var animalList = await _repo.FetchAll();
            if (animalList.Any())
                return animalList;

            return null;
        }
    }
}
