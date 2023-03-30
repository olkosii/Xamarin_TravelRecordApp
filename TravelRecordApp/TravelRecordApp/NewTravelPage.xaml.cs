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
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			Post post = new Post()
			{
				Experience = experienceEntry.Text
			};

			using (SQLiteConnection databaseConnection = new SQLiteConnection(App.DatabaseLocation))
			{
                databaseConnection.CreateTable<Post>();
				int rowsCount = 0;

				if (experienceEntry.Text != string.Empty && experienceEntry.Text != null)
                    rowsCount = databaseConnection.Insert(post);

				if (rowsCount > 0)
					DisplayAlert("Success", "Your experience was successfully inserted", "Ok");
				else
                    DisplayAlert("Failed", "Your experience failed to inserted", "Ok");

				experienceEntry.Text = string.Empty;
            }

			Navigation.PushAsync(new HomePage());
        }
    }
}