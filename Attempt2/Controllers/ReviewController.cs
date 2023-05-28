using Attempt2.Models;
using IMDbApiLib.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace Attempt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IMongoCollection<Review> _reviewscollection;
        public ReviewController(IMongoClient client)
        {
            var database = client.GetDatabase("ReviewsDb");
            _reviewscollection = database.GetCollection<Review>("Reviews");
        }
        [HttpGet("reviews/")]
        public async Task<string> GetReviews(string Title, int index)
        {
            var reviews = _reviewscollection.Find<Review>(r => r.MovieId == Title).ToList();
            if (reviews.Count == 0) return "No reviews";

            index = Math.Abs(index) % reviews.Count;

            return new string($"<b>{Title}</b>\n<b>{reviews[index].TgUsername.Split(" ").First()}</b> says:\n" +
                $"<i>{reviews[index].Text}</i>");
        }
        [HttpGet("GetMyReviewByIndex")]
        public async Task<string> GetMyReviewByIndex(string tgUsername,int index)
        {
            var reviews = _reviewscollection.Find<Review>(r => r.TgUsername == tgUsername).ToList();
            if (reviews.Count == 0) return "No reviews";

            index = Math.Abs(index) % reviews.Count;

            return new string($"<b>{reviews[index].MovieId}</b>\n" +
                $"<i>{reviews[index].Text}</i>");
        }
        [HttpPost("PostReview")]
        public async void PostAsync(string tgUsername,string movieId,int marking,string text="")
        {

            Review review = new Review()
            {
                TgUsername = tgUsername,
                Marking = marking,
                MovieId = movieId,
                Text = text
            };
            await _reviewscollection.InsertOneAsync(review);
        }
        [HttpDelete("DeleteReview")]
        public async void DeleteAsync(string tgUsername, string movieId)
        {
            await _reviewscollection.DeleteOneAsync(r => r.TgUsername == tgUsername && r.MovieId == movieId);
        }
    }
}
