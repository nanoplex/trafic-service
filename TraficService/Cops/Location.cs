using System;
using MongoDB.Bson;

namespace TraficService
{
    public class Location
    {
        public ObjectId _id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
    }
}
