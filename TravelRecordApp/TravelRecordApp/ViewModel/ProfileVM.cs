using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TravelRecordApp.Helpers.Services;
using TravelRecordApp.Models;

namespace TravelRecordApp.ViewModel
{
    internal class ProfileVM : INotifyPropertyChanged
    {
        public ObservableCollection<CategoryCount> Categories { get; set; }
        private int postCount;
        public int PostCount
        {
            get { return postCount; }
            set 
            {
                postCount = value; 
                OnPropertyChanged("PostCount");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileVM()
        {
            Categories = new ObservableCollection<CategoryCount>();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void GetPosts()
        {
            Categories.Clear();

            var posts = await FirestoreService.GetAll();
            PostCount = posts.Count();

            var categories = posts.Select(p => p.CategoryName).Distinct().ToList();

            foreach (var category in categories)
            {
                var count = posts.Where(p => p.CategoryName == category).ToList().Count();

                Categories.Add(new CategoryCount()
                {
                    Name = category,
                    Count = count
                });
            }
        }
    }

    public class CategoryCount
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
