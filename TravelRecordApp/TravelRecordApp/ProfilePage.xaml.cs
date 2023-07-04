using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private ProfileVM viewModel;
        public ProfilePage()
        {
            InitializeComponent();

            viewModel = Resources["viewModel"] as ProfileVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetPosts();
        }
    }
}