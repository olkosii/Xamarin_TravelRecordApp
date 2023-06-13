using Android.Gms.Maps.Model;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Interfaces;
using TravelRecordApp.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravelRecordApp.Droid.Dependencies.FirestoreServiceDroid))]
namespace TravelRecordApp.Droid.Dependencies
{
    public class FirestoreServiceDroid : Java.Lang.Object, IFirestoreService, IOnCompleteListener
    {
        private List<Post> posts;
        private bool hasReadPosts = false;
        public FirestoreServiceDroid()
        {
            posts = new List<Post>();
        }

        public async Task<List<Post>> GetAll()
        {
            try
            {
                hasReadPosts = false;

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 25; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);

                    if (hasReadPosts)
                        break;
                }

                return posts;
            }
            catch (Exception ex)
            {
                return posts;
            }
            
        }

        public async Task<bool> Update(Post post)
        {
            try
            {
                var document = new Dictionary<string, Java.Lang.Object>
                {
                    { "experience", post.Experience },
                    { "placeName", post.PlaceName },
                    { "distance", post.Distance },
                    { "categoryId", post.CategoryId },
                    { "categoryName", post.CategoryName },
                    { "latitude", post.Latitude },
                    { "longitude", post.Longitude },
                    { "address", post.Address },
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                };

                var posts = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                posts.Document(post.Id).Update(document);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Create(Post post)
        {
            try
            {
                var document = new Dictionary<string, Java.Lang.Object>
                {
                    { "experience", post.Experience },
                    { "placeName", post.PlaceName },
                    { "distance", post.Distance },
                    { "categoryId", post.CategoryId },
                    { "categoryName", post.CategoryName },
                    { "latitude", post.Latitude },
                    { "longitude", post.Longitude },
                    { "address", post.Address },
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                };

                var posts = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                posts.Add(new HashMap(document));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Post post)
        {
            try
            {
                var posts = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                posts.Document(post.Id).Delete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            try
            {
                if (task.IsSuccessful)
                {
                    var documents = (QuerySnapshot)task.Result;

                    posts.Clear();
                    foreach (var document in documents.Documents)
                    {
                        Post post = new Post()
                        {
                            Experience = document.Get("experience").ToString(),
                            PlaceName = document.Get("placeName").ToString(),
                            Distance = (int)document.Get("distance"),
                            CategoryId = document.Get("categoryId").ToString(),
                            CategoryName = document.Get("categoryName").ToString(),
                            Latitude = (double)document.Get("latitude"),
                            Longitude = (double)document.Get("longitude"),
                            Address = document.Get("address").ToString(),
                            UserId = document.Get("userId").ToString(),
                            Id = document.Id
                        };

                        posts.Add(post);
                    }
                }
                else
                    posts.Clear();

                hasReadPosts = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message);
            }
           
        }

    }
}