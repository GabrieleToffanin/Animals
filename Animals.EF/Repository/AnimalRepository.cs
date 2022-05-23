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
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;
        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Animal>> FetchAll()
        {
            return (await _context.Animals
                .Include(x => x.Specie)
                .ToListAsync())
                .OrderBy(x => x.Name)
                .AsQueryable();
        }

        //There should happen the wizardry of the "AutoCreation" for the specie related
        //to the entity if the specie does not exists already, otherwise add to the existing into the
        //Animals:ICollection model property.
        public async Task<bool> Create(Animal animal)
        {
            var foundSpecie = await _context.Species
                                        .Where(x => x.SpecieName.Equals(animal.Specie.SpecieName))
                                        .FirstOrDefaultAsync()
                                        .ConfigureAwait(false);
            if (animal.Id != 0) animal.Id = 0;

            if (foundSpecie != null)
            {
                animal.Specie = foundSpecie;
            }

            await _context.AddAsync(animal);
            if (await _context.SaveChangesAsync().ConfigureAwait(false) >= 0)
                return true;

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var animalToRemove = await _context.FindAsync(typeof(Animal), id) as Animal;

            if (animalToRemove is not null)
            {
                _context.Remove(animalToRemove);
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
        public async Task<bool> Update(Animal animal)
        {
            var result = _context.Animals.Select(b => b.Id == animal.Id).FirstOrDefault();
            if (!result)
                return false;

            _context.Animals.Update(animal);
            _context.Entry(animal).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Animal?> GetById(int id)
        {
            return _context.Animals
                   .Where(x => x.Id == id)
                   .Select(x => x).FirstOrDefault();
        }

        public async Task<IEnumerable<Animal>> GetStartingWithAnimals(Func<Animal, bool> filter)
        {
            var result = (await _context.Animals.Include(x => x.Specie)
                                                         .AsNoTracking()
                                                         .ToListAsync())
                                                         .Where(filter)
                                                         .OrderBy(x => x.Name);

            return result ?? Enumerable.Empty<Animal>();
                                                         
        }
    }
}
