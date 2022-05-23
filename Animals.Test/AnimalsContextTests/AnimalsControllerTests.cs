using Animals.Api.Controllers;
using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using Animals.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Net;

namespace Animals.Test.AnimalsContextTests
{
    public class AnimalsControllerTests : IClassFixture<CustomWebApplicationFactory<AnimalsController>>
    {
        private readonly CustomWebApplicationFactory<AnimalsController> _factory;
        private readonly HttpClient _client;

        public AnimalsControllerTests(CustomWebApplicationFactory<AnimalsController> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [InlineData("api/Animals")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Post_CreateAnimal_ActuallyCreates()
        {
            var response = await _client.PostAsJsonAsync("api/Animals", JsonConvert.SerializeObject(new AnimalCreationRequest()
            {
                Name = "Jaguar",
                AnimalHistoryAge = 1000,
                IsProtectedSpecie = true,
                Left = 100,
                Specie = "Mammal"
            }));
            var contentAfterResponse = await _client.GetAsync("api/Animals");
            var content = await contentAfterResponse.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<AnimalDTO>>(content);
            
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(1, items.Count());
        }
    }
}