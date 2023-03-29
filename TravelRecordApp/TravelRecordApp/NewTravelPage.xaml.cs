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
                int rows = databaseConnection.Insert(post);

				if (rows > 0)
					DisplayAlert("Success", "Your experience was successfully inserted", "Ok");
				else
                    DisplayAlert("Failed", "Your experience failed to inserted", "Ok");

				experienceEntry.Text = string.Empty;
            }
        }
    }
}