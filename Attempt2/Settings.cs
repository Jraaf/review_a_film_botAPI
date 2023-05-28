using Attempt2.Models;
using MongoDB.Driver;

namespace Attempt2
{
    public class Settings
    {
        public static string connectionUri = "mongodb+srv://MovieReview:kP6rak5X2yneVU3l@cluster0.mzifgtg.mongodb.net/?retryWrites=true&w=majority";
        public static MongoClient client=new MongoClient(connectionUri);
        public static IMongoDatabase db=client.GetDatabase("ReviewsDb");
        public static IMongoCollection<Review> ReviewsCollection = db.GetCollection<Review>("Reviews");
    }
}
