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
        IAsyncEnumerable<AnimalDTO> GetAllAnimals(string filter);
        Task<bool> Delete(int id);
        Task<bool> Create(AnimalCreationRequest animal);
        Task<bool> Update(int id, AnimalUpdateRequest animal);
    }
}
