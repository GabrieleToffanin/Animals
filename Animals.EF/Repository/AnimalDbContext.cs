using Animals.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Animals.EF.Repository
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Specie> Species { get; set; }

        public AnimalDbContext(DbContextOptions<AnimalDbContext> options) : base(options)
        {
        }

    }
}