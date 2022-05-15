using Animals.Core.Interfaces;
using Animals.Core.Models;
using Animals.EF.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.EF.Data
{
    public class AnimalRepository : IRepository
    {
        private readonly AnimalDbContext _context;
        public AnimalRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async ValueTask<IEnumerable<Animal>> FetchAll()
        {
            var items = await _context.Animals.ToListAsync();

            return items.AsQueryable();
        }

        public async ValueTask<bool> Create(Animal animal)
        {
            await _context.AddAsync(animal);
            if(await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async ValueTask<bool> Delete(int id)
        {
            var animalToRemove = await _context.FindAsync(typeof(Animal), id);

            if (animalToRemove is not null)
            {
                _context.Remove(animalToRemove);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async ValueTask<bool> Update(int id, Animal animal)
        {
            await UpdateAnimalAsync(id, animal);
            return true;
        }

        public async ValueTask<Animal?> GetById(int id)
        {
            return _context.Animals
                   .Where(x => x.Id == id)
                   .Select(x => x).FirstOrDefault() ?? null;
        }

        private async ValueTask UpdateAnimalAsync(int id, Animal animal)
        {
            var selected = await _context.Animals
                .Where(item => item.Id == id).FirstOrDefaultAsync();

            selected.Name = animal.Name;

            _context.Update(selected);

            await _context.SaveChangesAsync();
        }
    }
}
