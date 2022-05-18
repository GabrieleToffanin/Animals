using Animals.Core.Models;
using Animals.Core.Models.DTOInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface ISpecieBusinessLogic
    {
        IAsyncEnumerable<SpecieDTO> FetchSpecies();
        ValueTask CreateSpecieFromAnimal (AnimalDTO specieFromAnimal);
    }
}
