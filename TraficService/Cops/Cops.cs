using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TraficService
{
    public class Cops
    {
        private MongoCollection Collection { get { return new Mongo().Database.GetCollection("Cops"); } }

        public List<Location> Get() { return Collection.FindAllAs<Location>().ToList(); }

        public List<Location> GetInsideRadiusFromLocation(double selfLat, double selfLng, double radius, List<Location> cops)
        {   // convert radius to km
            return cops.Where(c => (Math.Pow((c.Latitude - selfLat), 2) + Math.Pow((c.Longitude - selfLng), 2) < Math.Pow(radius, 2))).ToList<Location>();
        }

        public void Add(Location cop) { Collection.Insert(cop); }

        public void Update(Location cop)
        {
            var query = Query<Location>.EQ(c => c._id, cop._id);
            var update = Update<Location>
                .Set(c => c.Latitude, cop.Latitude)
                .Set(c => c.Longitude, cop.Longitude)
                .Set(c => c.Date, cop.Date);

            Collection.Update(query, update);
        }

        public void Remove(ObjectId id)
        {
            var query = Query<Location>.EQ(c => c._id, id);
            Collection.Remove(query);
        }
    }
}
