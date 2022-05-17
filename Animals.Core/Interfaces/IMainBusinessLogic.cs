using Animals.Core.Models;
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
        ValueTask<IQueryable<Animal>> GetAllAnimals();
        ValueTask<bool> Delete(int id);
        ValueTask<bool> Create(AnimalDTO animal);
        ValueTask<bool> Update(Animal animal);
    }
}
