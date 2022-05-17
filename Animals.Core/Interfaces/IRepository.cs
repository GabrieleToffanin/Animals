using Animals.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Core.Interfaces
{
    public interface IRepository<TObject> where TObject : class 
    {
        ValueTask<IQueryable<TObject>> FetchAll();
        ValueTask<TObject> GetById(int id);
        ValueTask<bool> Create(TObject animal);
        ValueTask<bool> Delete(int id);
        ValueTask<bool> Update(TObject animal);
    }
}
