using Animals.Core.Models.Animals;
using Animals.Core.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.EF.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Specie> Species { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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

            base.OnModelCreating(modelBuilder);

        }
    }
}
