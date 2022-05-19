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
        Task<IQueryable<TObject>> FetchAll();
        Task<TObject> GetById(int id);
        Task<bool> Create(TObject animal);
        Task<bool> Delete(int id);
        Task<bool> Update(TObject animal);
    }
}
