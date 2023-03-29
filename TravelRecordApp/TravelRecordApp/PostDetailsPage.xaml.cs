﻿using SQLite;
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
	public partial class PostDetailsPage : ContentPage
	{
        private Post post;
		public PostDetailsPage (Post post)
		{
			InitializeComponent ();

            this.post = post;

            experienceEntry.Text = this.post.Experience;
		}

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            post.Experience = experienceEntry.Text;

            using (SQLiteConnection databaseConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                databaseConnection.CreateTable<Post>();
                int rows = databaseConnection.Update(post);

                if (rows > 0)
                    DisplayAlert("Success", "Your experience was successfully updated", "Ok");
                else
                    DisplayAlert("Failed", "Your experience failed to update", "Ok");
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection databaseConnection = new SQLiteConnection(App.DatabaseLocation))
            {
                databaseConnection.CreateTable<Post>();
                int rows = databaseConnection.Delete(post);

                if (rows > 0)
                    DisplayAlert("Success", "Your experience was successfully deleted", "Ok");
                else
                    DisplayAlert("Failed", "Your experience failed to delete", "Ok");

                Navigation.PushAsync(new HistoryPage());
            }
        }
    }
}