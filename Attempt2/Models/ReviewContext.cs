using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Attempt2.Models
{
    public class ReviewContext
    {
        private readonly IMongoDatabase _database = null;

        public ReviewContext(MongoClient client)
        {
            const string connectionUri = "mongodb+srv://MovieReview:kP6rak5X2yneVU3l@cluster0.mzifgtg.mongodb.net/?retryWrites=true&w=majority";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            _database = client.GetDatabase("ReviewsDb");
        }
    }
}
