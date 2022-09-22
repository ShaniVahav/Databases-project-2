
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
            var client = new MongoClient("mongodb+srv://localhost:27017@demodata-uymyo.mongodb.net/test?retryWrites=true&w=majority");
            // creation of the db
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");

            // creation of the collections
            var ingredients = database.GetCollection<BsonDocument> ("ingredients");
            var orders = database.GetCollection<BsonDocument> ("orders");
            var sales = database.GetCollection<BsonDocument> ("sales");
        }

        public static void insertObject_Sale(Sale s)
        {
           // List<BsonDocument> documents = new List<BsonDocument>();
             var document = new BsonDocument
                {                         
                    {"id_sale", Sale.id },
                    {"date", s.date},
                    {"price", s.price}  
                };
            // documents.Add(document);

            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument> ("sales");
            sales.InsertOne(document);
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
            int _id = Sale.getId();
            Console.WriteLine("\nid = " + _id + "\n");
            MongoClient client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("ice_cream_shop");
            var sales = database.GetCollection<BsonDocument>("sales");

            var _filter = Builders<BsonDocument>.Filter.Eq("id_sale", _id);
            var _update = Builders<BsonDocument>.Update.Set("price", _price);
            sales.UpdateOne(_filter, _update);
            Console.WriteLine("\nprice = " + _price);
        }

    }
}