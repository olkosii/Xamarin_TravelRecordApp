using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers.Interfaces;
using TravelRecordApp.Models;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravelRecordApp.iOS.Dependecies.FirestoreServiceIOS))]
namespace TravelRecordApp.iOS.Dependecies
{
    public class FirestoreServiceIOS : IFirestoreService
    {
        public bool Create(Post post)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("experience"),
                    new NSString("placeName"),
                    new NSString("distance"),
                    new NSString("categoryId"),
                    new NSString("categoryName"),
                    new NSString("latitude"),
                    new NSString("longitude"),
                    new NSString("address"),
                    new NSString("userId")
                };

                var values = new NSObject[]
                {
                    new NSString(post.Experience),
                    new NSString(post.PlaceName),
                    new NSNumber(post.Distance),
                    new NSString(post.CategoryId),
                    new NSString(post.CategoryName),
                    new NSNumber(post.Latitude),
                    new NSNumber(post.Longitude),
                    new NSString(post.Address),
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid)
                };

                var document = new NSDictionary<NSString, NSObject>(keys, values);

                var posts = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                posts.AddDocument(document);

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
                var posts = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                await posts.GetDocument(post.Id).DeleteDocumentAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Post>> GetAll()
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("postsDocuments");
                var query = collection.WhereEqualsTo("userId", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
                var postsDocuments = await query.GetDocumentsAsync();

                var postsList = new List<Post>();
                foreach (var document in postsDocuments.Documents)
                {
                    var dictionary = document.Data;

                    var post = new Post()
                    {
                        Experience = dictionary.ValueForKey(new NSString("experience")) as NSString,
                        PlaceName = dictionary.ValueForKey(new NSString("placeName")) as NSString,
                        Distance = (int)(dictionary.ValueForKey(new NSString("distance")) as NSNumber),
                        CategoryId = dictionary.ValueForKey(new NSString("categoryId")) as NSString,
                        CategoryName = dictionary.ValueForKey(new NSString("categoryName")) as NSString,
                        Latitude = (double)(dictionary.ValueForKey(new NSString("latitude")) as NSNumber),
                        Longitude = (double)(dictionary.ValueForKey(new NSString("longitude")) as NSNumber),
                        Address = dictionary.ValueForKey(new NSString("address")) as NSString,
                        UserId = dictionary.ValueForKey(new NSString("userId")) as NSString,
                        Id = document.Id
                    };

                    postsList.Add(post);
                }

                return postsList;
            }
            catch (Exception)
            {
                return new List<Post>();
            }
        }

        public async Task<bool> Update(Post post)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("experience"),
                    new NSString("placeName"),
                    new NSString("distance"),
                    new NSString("categoryId"),
                    new NSString("categoryName"),
                    new NSString("latitude"),
                    new NSString("longitude"),
                    new NSString("address"),
                    new NSString("userId")
                };

                var values = new NSObject[]
                {
                    new NSString(post.Experience),
                    new NSString(post.PlaceName),
                    new NSNumber(post.Distance),
                    new NSString(post.CategoryId),
                    new NSString(post.CategoryName),
                    new NSNumber(post.Latitude),
                    new NSNumber(post.Longitude),
                    new NSString(post.Address),
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid)
                };

                var document = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                await collection.GetDocument(post.Id).UpdateDataAsync(document);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}