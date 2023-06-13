using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Experience { get; set; }
        public string PlaceName { get; set; }
        public int Distance { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }     
    }
}
