using Animals.Core.Models.Animals;
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
        IAsyncEnumerable<SpecieDTO> FetchSpeciesWithFilter(Func<Specie, bool> filter); 
    }
}
