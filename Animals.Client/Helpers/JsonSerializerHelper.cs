using Animals.Client.Model;
using Animals.Client.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Client.Helpers
{
    public class JsonSerializerHelper : IJsonSerializer
    {
        
        private readonly IAPICallerService _apiService;

        public JsonSerializerHelper(IAPICallerService apiService)
        {
            
            _apiService = apiService;
        }
        public async ValueTask<IEnumerable<Animal>> FetchAnimals()
        {
            
                var strinJson = await _apiService.FetchAnimals();

                var content = JsonConvert.DeserializeObject<IEnumerable<Animal>>(strinJson);
                if (content is null)
                    return Enumerable.Empty<Animal>();

                return content;
            
        }
    }
}
