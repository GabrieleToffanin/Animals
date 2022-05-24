using Animals.Api.Controllers;
using Animals.Core.Interfaces;
using Animals.Core.Models.Animals;
using Animals.Core.Models.DTOInputModels;
using Animals.Core.Models.User;
using Animals.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Animals.Test.AnimalsContextTests
{
    //Probably this is more a BDD Test
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
            Assert.True(items.Count() == 1);
        }

        
        private async Task<string> GetToken(string username, string password)
        {
            var user = new TokenRequestModel
            {
                Email = username,
                Password = password
            };

            var res = await _client.PostAsJsonAsync("/Token", user);

            if (!res.IsSuccessStatusCode) return null;

            var userModel = await res.Content.ReadAsAsync<AuthenticationModel>();

            return userModel?.Token!;
        }

        private async Task<string> RegisterUser(string username, string password)
        {
            var user = new RegisterModel
            {
                Email = username,
                FirstName = "Gabriele",
                LastName = "Toffanin",
                Password = password,
                Username = "CptCrawler"
            };

            var res = await _client.PostAsJsonAsync("/Register", user);

            var roleAdding = new AddRoleModel
            {
                Email = username,
                Password = password,
                Role = "Administrator"
            };

            var result = await _client.PostAsJsonAsync("/AddRole", roleAdding);

            if (!res.IsSuccessStatusCode) return null;
            if (!result.IsSuccessStatusCode) return null;

            var userModel = await res.Content.ReadAsStringAsync();

            return userModel;
        }

        [Fact]
        public async Task GetUsersJwtInvalidTokenShouldReturnUnauth()
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "invalid_token");
            var response = await _client.PostAsJsonAsync("api/Animals", JsonConvert.SerializeObject(new AnimalCreationRequest()
            {
                Name = "Jaguar",
                AnimalHistoryAge = 1000,
                IsProtectedSpecie = true,
                Left = 100,
                Specie = "Mammal"
            }));
            var contentAfterResponse = await _client.GetAsync("api/Animals");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        }

        [Fact]
        public async Task GetUsersJwtValidTokenSouldReturnOk()
        {
            var registered = await RegisterUser("GabrieleToffanin@outlook.it", "Albero-12");
            var token = await GetToken("GabrieleToffanin@outlook.it", "Albero-12");

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var currentJsonPost = JsonConvert.SerializeObject(new AnimalCreationRequest()
            {
                Name = "Jaguar",
                AnimalHistoryAge = 1000,
                IsProtectedSpecie = true,
                Left = 100,
                Specie = "Mammal"
            });
            HttpContent content = new StringContent(currentJsonPost, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/Animals", content);
            var contentAfterResponse = await _client.GetAsync("api/Animals");
            var content = await contentAfterResponse.Content.ReadAsStringAsync();

            var items = JsonConvert.DeserializeObject<IEnumerable<AnimalDTO>>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(items.Count() == 2);
        }
    }
}
