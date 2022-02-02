using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Console_MongoDB
{
    public class Program
    {        

        // replace with your connection string if it is different
        const string MongoDBConnectionString = "mongodb://localhost";

        public static void Main(string[] args)
        {

            try
            {
                // Create client connection to our MongoDB database
                var client = new MongoClient(MongoDBConnectionString);
                // Create the collection object that represents the "products" collection
                var database = client.GetDatabase("MongoDBStore");
                
				var database = client.GetDatabase("1001670_campaign-tool_u1");
                var users = database.GetCollection<UserEntity>("users");

                var bquery = new BsonDocument("userRoles.role_id", new ObjectId("61faf1272fbc59efe0ac40db"));
                var filter = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(bquery);
                var listUser =  users
                        .Find(filter)
                        .ToList();


                Console.WriteLine("Fim");
				
				/* var products = database.GetCollection<Product>("products");

                // Create some sample data
                var tv = new Product
                {
                    Description = "Television",
                    SKU = 4001,
                    Price = 2000
                };
                var book = new Product
                {
                    Description = "A funny book",
                    SKU = 43221,
                    Price = 19.99
                };
                var dogBowl = new Product
                {
                    Description = "Bowl for Fido",
                    SKU = 123,
                    Price = 40.00
                };

                products.InsertOne(tv);
                products.InsertOne(book);
                products.InsertOne(dogBowl);

                var resultsBeforeUpdates = products
                                        .Find(Builders<Product>.Filter.Empty)
                                        .ToList();
                Console.WriteLine("Original Prices:\n");
                foreach (Product d in resultsBeforeUpdates)
                {
                    Console.WriteLine(
                                String.Format("Product Name: {0}\tPrice: {1:0.00}\tId: {2}",
                                    d.Description, d.Price, d.Id)
                    );
                } */
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            

            Console.WriteLine("Finished updating the product collection");
            Console.ReadKey();
        }
		
		public class Product
        {
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonElement("")]
            public int SKU { get; set; }
            [BsonElement("description")]
            public string Description { get; set; }
            [BsonElement("price")]
            public Double Price { get; set; }
        }
		
		public class UserEntity : BaseEntity
        {
            [BsonElement("name")]
            public string Name { get; set; }
            [BsonElement("user_id")]
            public string UserId { get; set; }
            [BsonElement("email")]
            public string Email { get; set; }
            [BsonElement("title")]
            public string Title { get; set; }
            [BsonElement("domainName")]
            public string DomainName { get; set; }
            [BsonElement("destinationIndicator")]
            public string DestinationIndicator { get; set; }
            [BsonElement("userRoles")]
            public List<UserRolesEntity> UserRolesEntity { get; set; }


        }

        public class BaseEntity
        {
            [BsonId]
            public ObjectId InternalId { get; set; }
        }

        public class UserRolesEntity
        {
            [BsonElement("role_id")]
            public ObjectId RoleId { get; set; }
            [BsonElement("insertedBy")]
            public ObjectId InsertedBy { get; set; }
            [BsonElement("inserted")]
            public DateTime Inserted { get; set; }
        }
    }
}
