using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    internal class PostDetailsVM
    {
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Post SelectedPost { get; set; }

        public PostDetailsVM()
        {
            UpdateCommand = new Command<string>(Update, CanUpdate);
            DeleteCommand = new Command(Delete);
        }

        private async void Update(string newExperience)
        {
            SelectedPost.Experience = newExperience;

            var result = await FirestoreService.Update(SelectedPost);

            if (result)
                await App.Current.MainPage.DisplayAlert("Success", "Your experience was successfully updated", "Ok");
            else
                await App.Current.MainPage.DisplayAlert("Failed", "Your experience failed to update", "Ok");

            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        private bool CanUpdate(string newExperience)
        {
            return string.IsNullOrWhiteSpace(newExperience) ? false : true;
        }

        private async void Delete()
        {
            var result = await FirestoreService.Delete(SelectedPost);

            if (result)
                await App.Current.MainPage.DisplayAlert("Success", "Your experience was successfully deleted", "Ok");
            else
                await App.Current.MainPage.DisplayAlert("Failed", "Your experience failed to delete", "Ok");

            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
    }
}
