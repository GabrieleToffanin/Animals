using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface IMainBusinessLogic
    {
        IAsyncEnumerable<AnimalDTO> GetAllAnimals();
        IAsyncEnumerable<AnimalDTO> GetAnimalsByFilter(Func<Animal, bool> filter);
        Task<bool> Delete(int id);
        Task<bool> Create(AnimalDTO animal);
        Task<bool> Update(int id, AnimalUpdateRequest animal);
    }
}
