using Animals.Client.Model;
using Animals.Client.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals.Client.ViewModel
{
    public sealed class MainPageViewModel : ObservableRecipient
    {
        public readonly IAsyncRelayCommand _animalLoaderCommand;
        //Actually this service is violating S principle 
        //see comment in Helpers/JSonSerializerHelper.cs
        private readonly IJsonSerializer _serializer;
        private readonly IAPICallerService _apiCallerService;

        public MainPageViewModel(IJsonSerializer serializer, IAPICallerService aPICallerService)
        {
            _serializer = serializer;
            _apiCallerService = aPICallerService;
            _animalLoaderCommand = new AsyncRelayCommand(LoadAnimalCollection);
        }

        public ObservableCollection<Animal> Animals { get; set; } = new ObservableCollection<Animal>();

        private Animal _selectedAnimal;

        public Animal SelectedAnimal
        {
            get => _selectedAnimal;
            set => SetProperty(ref _selectedAnimal, value);
        }


        private async Task LoadAnimalCollection()
        {
            var content = await Task.Run(async () => await _apiCallerService.FetchAnimals())
                                    .ContinueWith(async x => await _serializer.FetchAnimals(await x));

            foreach (var item in await content)
                Animals.Add(item);
        } 

    }
}
