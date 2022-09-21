
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
            // connection
            var client = new MongoClient("mongodb://localhost:27017");
            // creation of the db
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");

            // creation of the collections
            var ingredients = database.GetCollection<BsonDocument> ("ingredients");
            var orders = database.GetCollection<BsonDocument> ("orders");
            var sales = database.GetCollection<BsonDocument> ("sales");
        }

        public static void insertObject_Sale(Sale s)
        {
            List<BsonDocument> documents = new List<BsonDocument>();
             var document = new BsonDocument
                { 
                    {
                     "sale", new BsonDocument
                        {
                            {"id", Sale.id },
                            {"date", s.date},
                            {"price", s.price}
                        }
                    }
                };
            documents.Add(document);

            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument> ("sales");
            sales.InsertMany(documents);
        }


        public static void insertObjectToOrders_mongo(iceCreamOrder a, int round_number)
        {
            try
            {
                int id = Sale.getId();
                List<BsonDocument> documents = new List<BsonDocument>();

                // insert package
                var document = new BsonDocument
                    { 
                        {
                        "package", new BsonDocument
                            {
                                {"id_order", id },
                                {"round_number", round_number},
                                {"id_ingredient", a.package},
                                {"amount", 1}
                            }
                        }
                    };
                documents.Add(document);

                // insert flavours 
                foreach(var item in a.fdict)
                {
                    int k = item.Key;
                    int v = item.Value;
                    if(v == 0 || k == 11 )
                        continue;

                    document = new BsonDocument
                    { 
                        {
                        "flavour", new BsonDocument
                            {
                                {"id_order", id },
                                {"round_number", round_number},
                                {"id_ingredient", k},
                                {"amount", v}
                            }
                        }
                    };

                documents.Add(document);
                } 


                // insert toppings         
                foreach(int item in a.toppings)
                { 
                    document = new BsonDocument
                    { 
                        {
                        "topping", new BsonDocument
                            {
                                {"id_order", id },
                                {"round_number", round_number},
                                {"id_ingredient", item},
                                {"amount", 1}
                            }
                        }
                    };

                    documents.Add(document);
                }

                var client = new MongoClient("mongodb://localhost:27017");
                IMongoDatabase database = client.GetDatabase("ice_cream_shop");
                var orders = database.GetCollection<BsonDocument> ("orders");
                orders.InsertMany(documents);    
            }

            catch (Exception ex)
            {
                {   
                    Console.WriteLine("insert object func");
                    Console.WriteLine(ex.ToString());
                }

            }

        }

        public static void update_price_mongo(int _price)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument> ("sales");
            sales.updateOne({id:Sale.getId()}, {$set: {price:_price}});
        }


        // public static void fillDocuments (List<MongoOrder> orders) //to make it by interface, rename to fillData
        // {

        //      List<BsonDocument> documents = new List<BsonDocument>();

        //     //build list of all documents
        //     foreach (var mongoOrderd in orders) {
        //         var document = new BsonDocument
        //         { 
        //             {
        //                 "viecle", new BsonDocument 
        //                 { 
        //                     { "owner", new BsonDocument { 
        //                         {"Name", mongoOrderd.GetOwner().getName() },
        //                         {"Phone",mongoOrderd.GetOwner().getPhone()},
        //                         {"Addres",mongoOrderd.GetOwner().getAddress()} }},
        //                     { "model", mongoOrderd.getVehicle().getManufacturer() },
        //                     { "year" , mongoOrderd.getVehicle().getYear() },
        //                     { "color", mongoOrderd.getVehicle().getColor() } 
        //                 }
        //             }, 
        //             { 
        //                 "task", new BsonDocument 
        //                 { 
        //                     { "name", mongoOrderd.getVtask().getName() },
        //                     { "description" , mongoOrderd.getVtask().getDescription() },
        //                     { "price", mongoOrderd.getVtask().getPrice() } 
        //                 }
        //             }, 
        //             { "orderd_date", mongoOrderd.getOrderDate() },
        //             { "complete_date",DateTime.Now},
        //             { "completed", mongoOrderd.getCompleted() },
        //             { "payed", mongoOrderd.getPayed() }
        //         };

        //         documents.Add(document);
            
        //     }

        //     Console.WriteLine("list is ok");
        //     //add them all to mongo

        //     var settings = MongoClientSettings.FromConnectionString("mongodb+srv://user:password@cluster.mongodb.net/?retryWrites=true&w=majority");
        //     settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        //     var client = new MongoClient(settings);
        //     var database = client.GetDatabase("garage");
        //     var collection = database.GetCollection<BsonDocument> ("orders");

           
        //     collection.InsertMany(documents);
        //     //await collection.InsertOneAsync (document);
        // }
    }
}