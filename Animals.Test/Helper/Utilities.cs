using Animals.Core.Models.Animals;
using Animals.EF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Test.Helper
{
    public static class Utilities
    {
        public static void InitializeDbForTest(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Animals.AddRange(applicationDbContext.Animals);
            applicationDbContext.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Animals.RemoveRange(db.Animals);
            InitializeDbForTest(db);
        }

        public static List<Animal> GetSeedingAnimals()
        {
            return new List<Animal>
            {
                new Animal
                {
                    Name = "Tiger",
                    AnimalHistoryAge = 1000,
                    Id = 1,
                    IsProtectedSpecie = true,
                    Left = 500,
                    Specie = new Specie
                    {
                        Animals = new List<Animal> { },
                        SpecieId = 1,
                        SpecieName = "Mammal",
                        TypeOfBirth = "Pregnancy"
                    },
                    SpecieId = 1

                }
            };
        }
    }
}
