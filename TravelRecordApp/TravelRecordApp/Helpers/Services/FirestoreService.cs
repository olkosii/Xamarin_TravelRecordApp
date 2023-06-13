using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Interfaces;
using TravelRecordApp.Models;
using Xamarin.Forms;

namespace TravelRecordApp.Helpers.Services
{
    internal class FirestoreService
    {
        private static IFirestoreService firestoreService = DependencyService.Get<IFirestoreService>();

        public static bool Create(Post post)
        {
            try
            {
                return firestoreService.Create(post);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> Update(Post post)
        {
            try
            {
                return await firestoreService.Update(post);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> Delete(Post post)
        {
            try
            {
                return await firestoreService.Delete(post);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Post>> GetAll()
        {
            try
            {
                return await firestoreService.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
