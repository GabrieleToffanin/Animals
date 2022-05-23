using Animals.Api.Controllers;
using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using Animals.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

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
    }
}