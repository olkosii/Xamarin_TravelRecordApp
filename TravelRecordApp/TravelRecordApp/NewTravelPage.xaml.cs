using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using TravelRecordApp.Repositories;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            if (!locator.IsListening)
                await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);

            var position = await locator.GetPositionAsync();

			var places = await PlaceRepository.GetPlacesAsync(position.Latitude, position.Longitude);
			placesListView.ItemsSource = places.results;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			try
			{
                var selectedPlace = placesListView.SelectedItem as Result;
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = selectedPlace.categories.FirstOrDefault().id.ToString(),
                    CategoryName = selectedPlace.categories.FirstOrDefault().name,
                    Address = selectedPlace.location.address,
                    Latitude = selectedPlace.geocodes.main.latitude,
                    Longitude = selectedPlace.geocodes.main.longitude,
                    Distance = selectedPlace.distance,
                    PlaceName = selectedPlace.name
                };

                //using (SQLiteConnection databaseConnection = new SQLiteConnection(App.DatabaseLocation))
                //{
                //    databaseConnection.CreateTable<Post>();
                //    int rowsCount = 0;

                //    if (experienceEntry.Text != string.Empty && experienceEntry.Text != null)
                //        rowsCount = databaseConnection.Insert(post);

                //    if (rowsCount > 0)
                //        DisplayAlert("Success", "Your experience was successfully inserted", "Ok");
                //    else
                //        DisplayAlert("Failed", "Your experience failed to inserted", "Ok");

                //    experienceEntry.Text = string.Empty;
                //}

                var result = false;
                if (experienceEntry.Text != string.Empty && experienceEntry.Text != null)
                    result = FirestoreService.Create(post);
                    

                if (result)
                    DisplayAlert("Success", "Your experience was successfully inserted", "Ok");
                else
                    DisplayAlert("Failed", "Your experience failed to inserted", "Ok");

                experienceEntry.Text = string.Empty;

                Navigation.PushAsync(new HomePage());
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
        }
    }
}