using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace TraficService
{
    class Mongo
    {
        public MongoDatabase Database { get; private set;  }

        public Mongo()
        {
            var connectionString = "mongodb://93.160.108.34";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            Database = server.GetDatabase("TraficService");
        }
    }
}
