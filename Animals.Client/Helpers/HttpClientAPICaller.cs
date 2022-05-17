using Animals.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Client.Helpers
{
    public class HttpClientAPICaller : IAPICallerService
    {
        //Maybe put the baseApiUri into appsetting.json (?)
        private readonly Uri baseApiUri = new Uri("https://localhost:7098");
        
        public async Task<string> FetchAnimals()
        {

            using (var _httpClient = new HttpClient())
            {
                
                var response = await _httpClient.GetAsync(new Uri(baseApiUri + "/AnimalsInfo"));
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
