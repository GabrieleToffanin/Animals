using Animals.Api.Controllers;
using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Animals.Test.AnimalsContextTests
{
    public class AnimalsControllerTests
    {
        [Fact]
        public async void IndexReturnsJsonWithAListOfAnimals()
        {
            var mockRepo = new Mock<IMainBusinessLogic>();
            mockRepo.Setup(repo => repo.GetAllAnimals("")).Returns(GetTestAnimals());

            var controller = new AnimalsController(mockRepo.Object);

            var result = await controller.FetchAll("");

            var actionResult = Assert.IsType<ActionResult<IAsyncEnumerable<AnimalDTO>>>(result);

            int count = 0;
            await foreach (var item in mockRepo.Object.GetAllAnimals(""))
            {
                Assert.IsType<AnimalDTO>(item);
                count++;
            }

            Assert.Equal(2, count);
        }

        private async IAsyncEnumerable<AnimalDTO> GetTestAnimals()
        {
            var animals = new List<AnimalDTO>();

            animals.Add(new AnimalDTO
            {
                AnimalHistoryAge = 100,
                IsProtectedSpecie = true,
                Left = 100,
                Name = "Tiger",
                Specie = "Mammal"
            });
            animals.Add(new AnimalDTO
            {
                AnimalHistoryAge = 100,
                IsProtectedSpecie = true,
                Left = 100,
                Name = "Tiger",
                Specie = "Mammal"
            });


            foreach (var animal in animals)
                yield return animal;
        }
    }
}