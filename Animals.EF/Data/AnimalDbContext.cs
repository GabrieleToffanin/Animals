using Animals.Core.Models.Animals;
using Microsoft.EntityFrameworkCore;

namespace Animals.EF.Data
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Specie> Species { get; set; }

        public AnimalDbContext(DbContextOptions<AnimalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specie>()
                .HasMany(c => c.Animals)
                .WithOne(c => c.Specie)
                .OnDelete(DeleteBehavior.ClientCascade);



            modelBuilder.Entity<Animal>()
                .HasOne(c => c.Specie)
                .WithMany(c => c.Animals)
                .HasForeignKey(c => c.SpecieId);

        }


    }
}