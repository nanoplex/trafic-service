using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TraficService
{
    public class Persons
    {
        private MongoCollection collection { get { return new Mongo().Database.GetCollection("Persons"); } }

        public List<Person> Get()
        {
            return collection.FindAllAs<Person>().ToList();
        }

        public void Add(Person person)
        {
            collection.Insert(person);
        }

        public void Update(Person person)
        {
            var query = Query<Person>.EQ(p => p._id, person._id);

            var update = Update<Person>
                .Set(p => p.Name, person.Name)
                .Set(p => p.Age, person.Age)
                .Set(p => p.Gender, person.Gender);

            collection.Update(query, update);
        }

        public void Delete(Person person)
        {
            var query = Query<Person>.EQ(p => p._id, person._id);
            collection.Remove(query);
        }

        public class Person
        {
            public ObjectId _id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public bool Gender { get; set; }
        }
    }
    
}
