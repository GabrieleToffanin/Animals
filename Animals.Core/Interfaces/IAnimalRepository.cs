using Animals.Core.Models.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Task<IEnumerable<Animal>> GetStartingWithAnimals(Func<Animal, bool> filter);
    }
}
