using SQLite;
using System;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryVM _viewModel;
        public HistoryPage()
        {
            InitializeComponent();

            _viewModel = Resources["viewModel"] as HistoryVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.GetPosts();
        }
    }
}