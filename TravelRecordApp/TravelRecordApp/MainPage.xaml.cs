using System;
using TravelRecordApp.Helpers.Services;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.travel.png");
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailEntry.Text) || string.IsNullOrEmpty(passwordEntry.Text)){}
            else
            {
                var loginResult = await AuthService.LoginUser(emailEntry.Text, passwordEntry.Text);

                if(loginResult)
                    await Navigation.PushAsync(new HomePage());
            }
        }
    }
}
