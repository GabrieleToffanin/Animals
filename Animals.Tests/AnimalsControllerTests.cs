using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Moq;

namespace Animals.Tests
{
    public class AnimalsControllerTests
    {
        [Fact]
        public void Can_Use_Animals_Repository()
        {
            Mock<IAnimalRepository> animalsMock = new Mock<IAnimalRepository>();

            animalsMock.Setup(x => x.FetchAll()).Returns((
                new Animal { Id = 1, Name = "Tiger", Specie = new Specie { SpecieId = 1, Animals = new List<Animal> { } }, SpecieId = 1}))
        }
    }
}