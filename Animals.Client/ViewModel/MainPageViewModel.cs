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
        private readonly IJsonSerializer _serializer;

        public MainPageViewModel(IJsonSerializer serializer)
        {
            _serializer = serializer;
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
            var content = await _serializer.FetchAnimals();
            foreach (var item in content)
                Animals.Add(item);
        } 

    }
}
