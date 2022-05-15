using Animals.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Client.Services
{
    public interface IJsonSerializer
    {
        ValueTask<IEnumerable<Animal>> FetchAnimals();
    }
}
