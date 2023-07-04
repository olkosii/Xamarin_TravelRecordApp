using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    internal class NewTravelVM : INotifyPropertyChanged
    {
        public ObservableCollection<Result> PlacesCollection { get; set; }
        public Command SaveCommand { get; set; }

        private bool _postIsReady;
        public bool PostIsReady
        {
            get { return !string.IsNullOrEmpty(Experience) && SelectedPlace != null; }
        }

        private Result _selectedPlace;
        public Result SelectedPlace
        {
            get { return _selectedPlace; }
            set 
            {
                _selectedPlace = value;
                OnPropertyChanged("PostIsReady");
            }
        }

        private string _experience;
        public string Experience
        {
            get { return _experience; }
            set
            { 
                _experience = value;
                OnPropertyChanged("Experience");
                OnPropertyChanged("PostIsReady");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public NewTravelVM()
        {
            PlacesCollection = new ObservableCollection<Result>();
            SaveCommand = new Command<bool>(Save, CanSave);
        }

        private void Save(bool parameter)
        {
            try
            {
                Post post = new Post()
                {
                    Experience = Experience,
                    CategoryId = SelectedPlace.categories.FirstOrDefault().id.ToString(),
                    CategoryName = SelectedPlace.categories.FirstOrDefault().name,
                    Address = SelectedPlace.location.address,
                    Latitude = SelectedPlace.geocodes.main.latitude,
                    Longitude = SelectedPlace.geocodes.main.longitude,
                    Distance = SelectedPlace.distance,
                    PlaceName = SelectedPlace.name
                };

                var result = false;
                if (Experience != string.Empty && Experience != null)
                    result = FirestoreService.Create(post);


                if (result)
                    App.Current.MainPage.DisplayAlert("Success", "Your experience was successfully inserted", "Ok");
                else
                    App.Current.MainPage.DisplayAlert("Failed", "Your experience failed to inserted", "Ok");

                Experience = string.Empty;

                App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool CanSave(bool parameter)
        {
            return PostIsReady;
        }

        public async void GetPlaces(double latitude, double longitude)
        {
            var places = await PlaceService.GetPlacesAsync(latitude, longitude);

            PlacesCollection.Clear();

            foreach (var place in places.results)
            {
                PlacesCollection.Add(place);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
