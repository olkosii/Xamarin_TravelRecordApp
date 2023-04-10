﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Models
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string Experience { get; set; }
        public string PlaceName { get; set; }
        public int Distance { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }
}
