using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        IGeolocator locator = CrossGeolocator.Current;

        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetLocation();

            GetPosts();
        }

        private async void GetPosts()
        {
            //using (var connection = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    connection.CreateTable<Post>();
            //    var posts = connection.Table<Post>().ToList();

            //    DisplayPostsOnMap(posts);
            //}

            var posts = await FirestoreService.GetAll();

            DisplayPostsOnMap(posts);
        }

        private void DisplayPostsOnMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var pinCoordinates = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Pin()
                    {
                        Label = post.PlaceName,
                        Position = pinCoordinates,
                        Address = post.Address,
                        Type = PinType.SavedPin
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            locator.StopListeningAsync();
        }

        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(new TimeSpan(0,1,0),100);

                locationsMap.IsShowingUser = true;

                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
            var center = new Xamarin.Forms.Maps.Position(latitude, longitude);

            locationsMap.MoveToRegion(new MapSpan(center, 1, 1));
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                //prompt user to turn on the permission in settings
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            switch (locationsMap.MapType)
            {
                case Xamarin.Forms.Maps.MapType.Street:
                    locationsMap.MapType = Xamarin.Forms.Maps.MapType.Satellite;
                    break;
                case Xamarin.Forms.Maps.MapType.Satellite:
                    locationsMap.MapType = Xamarin.Forms.Maps.MapType.Hybrid;
                    break;
                case Xamarin.Forms.Maps.MapType.Hybrid:
                    locationsMap.MapType = Xamarin.Forms.Maps.MapType.Street;
                    break;
                default:
                    locationsMap.MapType = Xamarin.Forms.Maps.MapType.Hybrid;
                    break;
            }
        }
    }
}