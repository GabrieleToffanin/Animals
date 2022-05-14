using Animals.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Animals.EF.Repository
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public AnimalDbContext(DbContextOptions<AnimalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasData(
                new Animal
                {
                    Id = 1,
                    Name = "Tiger"
                });
        }

    }
}