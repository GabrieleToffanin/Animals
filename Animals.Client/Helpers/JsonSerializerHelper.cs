﻿using Animals.Client.Model;
using Animals.Client.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Client.Helpers
{
    //IAPICallerService will be put into ViewModel, now it's present here just for testing,
    //FetchAnimals() : ValueTask<IEnumerable<Animal>> will be translated into a Deserialize generic method 
    //and it will have only the responsibility of Deserializing string retrieved from FetchAnimals():string method in 
    //IAPICallerService 
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
