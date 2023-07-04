using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    public class HistoryVM
    {
        public Command SelectedItem { get; set; }
        public Command NewTravelCommand { get; set; }
        public ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
            SelectedItem = new Command<Post>(ItemSelected);
            NewTravelCommand = new Command(NewTravelNavigation);
        }

        public void ItemSelected(Post post)
        {
            App.Current.MainPage.Navigation.PushAsync(new PostDetailsPage(post));
        }

        public void NewTravelNavigation()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }

        public async void GetPosts()
        {
            Posts.Clear();

            var posts = await FirestoreService.GetAll();

            foreach (var post in posts)
                Posts.Add(post);
        }
    }
}
