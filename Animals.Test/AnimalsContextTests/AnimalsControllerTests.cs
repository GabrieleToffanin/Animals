using Animals.Api.Controllers;
using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace Animals.Test.AnimalsContextTests
{
    public class AnimalsControllerTests : IClassFixture<WebApplicationFactory<AnimalsController>>
    {
        private readonly WebApplicationFactory<AnimalsController> _factory;

        public AnimalsControllerTests(WebApplicationFactory<AnimalsController> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("api/Animals")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}