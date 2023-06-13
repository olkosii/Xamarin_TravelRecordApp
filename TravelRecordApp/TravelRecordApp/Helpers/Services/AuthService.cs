using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Interfaces;
using Xamarin.Forms;

namespace TravelRecordApp.Helpers.Services
{
    internal class AuthService
    {
        private static IAuthService authService = DependencyService.Get<IAuthService>();

        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await authService.RegisterUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
        }

        public static async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                return await authService.LoginUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error",ex.Message,"Ok");

                if (ex.Message.Contains(Helpers.Constants.Messages.registerMessage))
                    return await authService.RegisterUser(email, password);

                return false;
            }
        }

        public static bool IsAuthenticated()
        {
            return authService.IsAuthenticated();
        }

        public static string GetCurrentUserId()
        {
            return authService.GetCurrentUserId();
        }
    }
}
