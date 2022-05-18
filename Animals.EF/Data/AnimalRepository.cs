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
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDbContext _context;
        public AnimalRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async ValueTask<IQueryable<Animal>> FetchAll()
        {
            return (await _context.Animals
                .Include(x=> x.Specie)
                .ToListAsync())
                .AsQueryable();
        }
        
        //There should happen the wizardry of the "AutoCreation" for the specie related
        //to the entity if the specie does not exists already, otherwise add to the existing into the
        //Animals:ICollection model property.
        public async ValueTask<bool> Create(Animal animal)
        {
            var foundSpecie = await _context.Species
                                        .Where(x => x.SpecieName.Equals(animal.Specie.SpecieName))
                                        .FirstOrDefaultAsync()
                                        .ConfigureAwait(false);
            if (foundSpecie != null)
            {
                animal.Specie = foundSpecie;
            }

            await _context.AddAsync(animal);
            if (await _context.SaveChangesAsync().ConfigureAwait(false) >= 0)
                return true;

            return false;
        }

        public async ValueTask<bool> Delete(int id)
        {
            var animalToRemove = await _context.FindAsync(typeof(Animal), id) as Animal;

            if (animalToRemove is not null)
            {
                _context.Remove<Animal>(animalToRemove);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        /// <summary>
        /// Updates with new animal Data recieved from Client
        /// </summary>
        /// <param name="animal">Current animal with already modified data</param>
        /// <returns>True if update success, otherwise false</returns>
        public async ValueTask<bool> Update(Animal animal)
        {
            var result = _context.Animals.Select(b => b.Id == animal.Id).FirstOrDefault();
            if (!result)
                return false;

            _context.Animals.Update(animal);
            _context.Entry(animal).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }

        public async ValueTask<Animal?> GetById(int id)
        {
            return _context.Animals
                   .Where(x => x.Id == id)
                   .Select(x => x).FirstOrDefault();
        }

        
    }
}
