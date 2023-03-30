using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection databaseConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                databaseConnection.CreateTable<Post>();
                var posts = databaseConnection.Table<Post>().ToList();

                postsListView.ItemsSource = posts;
            }
        }

        private void postsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (postsListView.SelectedItem is Post selectedPost)
            {
                Navigation.PushAsync(new PostDetailsPage(selectedPost));
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTravelPage());
        }
    }
}