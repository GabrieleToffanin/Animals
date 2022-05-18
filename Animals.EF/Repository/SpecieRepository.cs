using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.EF.Repository
{
    public class SpecieRepository : ISpecieRepository
    {
        private readonly AnimalDbContext _context;
        public SpecieRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async ValueTask<bool> Create(Specie specie)
        {
            await _context.AddAsync(specie);

            await _context.SaveChangesAsync();

            return true;
        }

        public ValueTask<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<IQueryable<Specie>> FetchAll()
        {
            return (await _context.Species.Include(x => x.Animals).AsNoTracking().ToListAsync()).AsQueryable();
        }

        public async ValueTask<Specie?> GetById(int id)
        {
            return _context.Species.Where(x => x.SpecieId == id)
                                   .Select(x => x).FirstOrDefault();
        }

        public async ValueTask<bool> Update(Specie animal)
        {
            var result = await _context.Species.Where(b => b.SpecieId == animal.SpecieId).FirstOrDefaultAsync();
            if (result == null)
                return false;
            _context.Species.Update(result);

            await _context.SaveChangesAsync();
            return true;

        }

        public async ValueTask<Specie> GetBySpecieName(string specieName)
            => await _context.Species.Where(x => x.SpecieName == specieName!).FirstOrDefaultAsync();

        public async ValueTask<IEnumerable<Specie>> GetAnimalsFromSpecieName(Func<Specie, bool> filter)
        {
            return (await _context.Species.Include(p => p.Animals)
                                          .AsNoTracking()
                                          .ToListAsync())
                                          .Where(filter)
                                          .AsQueryable();


        }
    }
}
