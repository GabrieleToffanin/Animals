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

        public async Task<bool> Create(Specie specie)
        {
            await _context.AddAsync(specie);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var toDeleteSpecie = await _context.Species.Where(x => x.SpecieId == id)
                                                              .FirstOrDefaultAsync();
            if (toDeleteSpecie != null)
                return false;

            _context?.Remove(toDeleteSpecie);
            return true;
        }

        public async Task<IQueryable<Specie>> FetchAll()
        {
            return (await _context.Species.Include(x => x.Animals).AsNoTracking().ToListAsync()).AsQueryable();
        }

        public async Task<Specie> GetById(int id)
        {
            return await _context.Species.Where(x => x.SpecieId == id)
                                         .Select(x => x)
                                         .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Specie animal)
        {
            var result = await _context.Species.Where(b => b.SpecieId == animal.SpecieId).FirstOrDefaultAsync();
            if (result == null)
                return false;
            _context.Species.Update(result);

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Specie> GetBySpecieName(string specieName)
            => await _context.Species.Where(x => x.SpecieName == specieName!).FirstOrDefaultAsync();

        public async Task<IEnumerable<Specie>> GetAnimalsFromSpecieName(Func<Specie, bool> filter)
        {
            return (await _context.Species.Include(p => p.Animals)
                                          .AsNoTracking()
                                          .ToListAsync())
                                          .Where(filter)
                                          .AsQueryable();


        }
    }
}
