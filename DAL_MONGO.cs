
using MongoDB.Driver;
using MongoDB.Bson;

using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MongoAccess
{
    class MongoAccess
    {
        public static void MongoTables()
        {
            Console.WriteLine("MongoTables");
            // connection 
            var settings = MongoClientSettings.FromConnectionString("mongodb://127.0.0.1:27017");         
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // creation of the db
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_shop");

            // creation of the collections
            // var ingredients = database.GetCollection<BsonDocument> ("ingredients");
            // var orders = database.GetCollection<BsonDocument> ("orders");
            // var sales = database.GetCollection<BsonDocument> ("sales");

            var dbList = database.ListCollectionNames().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }

        public static void fillDocuments (List<MongoOrder> orders) //to make it by interface, rename to fillData
        {

             List<BsonDocument> documents = new List<BsonDocument>();

            //build list of all documents
            foreach (var mongoOrderd in orders) {
                var document = new BsonDocument
                { 
                    {
                        "viecle", new BsonDocument 
                        { 
                            { "owner", new BsonDocument { 
                                {"Name", mongoOrderd.GetOwner().getName() },
                                {"Phone",mongoOrderd.GetOwner().getPhone()},
                                {"Addres",mongoOrderd.GetOwner().getAddress()} }},
                            { "model", mongoOrderd.getVehicle().getManufacturer() },
                            { "year" , mongoOrderd.getVehicle().getYear() },
                            { "color", mongoOrderd.getVehicle().getColor() } 
                        }
                    }, 
                    { 
                        "task", new BsonDocument 
                        { 
                            { "name", mongoOrderd.getVtask().getName() },
                            { "description" , mongoOrderd.getVtask().getDescription() },
                            { "price", mongoOrderd.getVtask().getPrice() } 
                        }
                    }, 
                    { "orderd_date", mongoOrderd.getOrderDate() },
                    { "complete_date",DateTime.Now},
                    { "completed", mongoOrderd.getCompleted() },
                    { "payed", mongoOrderd.getPayed() }
                };

                documents.Add(document);
            
            }

            Console.WriteLine("list is ok");
            //add them all to mongo

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://user:password@cluster.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("garage");
            var collection = database.GetCollection<BsonDocument> ("orders");

           
            collection.InsertMany(documents);
            //await collection.InsertOneAsync (document);
        }
    }
}