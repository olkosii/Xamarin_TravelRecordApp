using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Helpers.Services;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    internal class MainVM : INotifyPropertyChanged
    {
        public Command LoginCommand { get; set; }
        private string email;
        public string Email { 
            get { return email; } 
            set 
            { 
                email = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set 
            {
                password = value; 
                OnPropertyChanged("EntriesHaveText");
            }
        }
        public bool EntriesHaveText
        {
            get { return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainVM()
        {
            LoginCommand = new Command<bool>(Login,CanLogin);
        }

        public async void Login(bool parameter)
        {
            var loginResult = await AuthService.LoginUser(Email, Password);

            if (loginResult)
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        public bool CanLogin(bool parameter)
        {
            return EntriesHaveText;
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
