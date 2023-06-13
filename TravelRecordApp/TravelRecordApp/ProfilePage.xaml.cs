using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    var postTable = connection.Table<Post>().ToList();

            //    var categories = postTable.Select(p => p.CategoryName).Distinct().ToList();

            //    var categoriesCount = new Dictionary<string, int>();
            //    foreach (var category in categories) 
            //    {
            //        var count = postTable.Where(p => p.CategoryName == category).ToList().Count();

            //        categoriesCount.Add(category, count);
            //    }
            //    categoriesListView.ItemsSource = categoriesCount;

            //    postCountLabel.Text = postTable.Count.ToString();
            //}
            var postTable = await FirestoreService.GetAll();

            var categories = postTable.Select(p => p.CategoryName).Distinct().ToList();

            var categoriesCount = new Dictionary<string, int>();
            foreach (var category in categories)
            {
                var count = postTable.Where(p => p.CategoryName == category).ToList().Count();

                categoriesCount.Add(category, count);
            }
            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = postTable.Count.ToString();
        }
    }
}