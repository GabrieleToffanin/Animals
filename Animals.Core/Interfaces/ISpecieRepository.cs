using Animals.Core.Models.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface ISpecieRepository : IRepository<Specie>
    {

        Task<IEnumerable<Specie>> GetAnimalsFromSpecieName(Func<Specie, bool> filter);
    }
}
