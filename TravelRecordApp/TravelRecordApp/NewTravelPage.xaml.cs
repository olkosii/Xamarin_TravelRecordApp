using Plugin.Geolocator;
using SQLite;
using System;
using System.Linq;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
        private NewTravelVM viewModel;
		public NewTravelPage ()
		{
			InitializeComponent ();

            viewModel = Resources["viewModel"] as NewTravelVM;
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            if (!locator.IsListening)
                await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);

            var position = await locator.GetPositionAsync();

            viewModel.GetPlaces(position.Latitude, position.Longitude);
        }
    }
}