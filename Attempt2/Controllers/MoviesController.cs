using Attempt2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using IMDbApiLib;
using IMDbApiLib.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Net.WebRequestMethods;
using Amazon.Runtime.Internal.Transform;

namespace Attempt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {

        [HttpGet("{Title}")]
        public async Task<ActionResult<Attempt2.Models.SearchData>> GetTitle(string Title)
        {
            IMDbApiLib.Models.AdvancedSearchLanguage language = IMDbApiLib.Models.AdvancedSearchLanguage.English;
            if (Regex.IsMatch(Title.ToString(), "[а-я]", RegexOptions.IgnoreCase))
            {
                language = IMDbApiLib.Models.AdvancedSearchLanguage.Ukrainian;
            }
            var apiLib = new ApiLib("k_1yc2vol9");
            var data = await apiLib.AdvancedSearchAsync(new IMDbApiLib.Models.AdvancedSearchInput()
            {
                Title = Title,
                Languages = language
            });
            Movie movie = new Movie()
            {
                Id = data.Results[0].Id,
                Image = data.Results[0].Image,
                Title = data.Results[0].Title,
                Description = data.Results[0].Description,
                IMDbRating = data.Results[0].IMDbRating,
                Stars = data.Results[0].Stars,
                Genres = data.Results[0].Genres
            };
            string watchlink;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://streaming-availability.p.rapidapi.com/v2/search/title?title={Title}&country=us&show_type=movie&output_language=en"),
                Headers =
            {
                { "X-RapidAPI-Key", "7e1c129c19mshcb1c97363c15879p1ee493jsn823740fce30c" },
                { "X-RapidAPI-Host", "streaming-availability.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                watchlink = body.Split("\"link\":")[1].Split("\"")[1];       

                Console.WriteLine(body);
            }


            string result = $"{movie.Title} \n{movie.Description}\n{movie.Genres}\n" +
                $"<b>Cast:</b> {movie.Stars}\n" +
                $"<b>IMDb rating:</b>" +
                $"{movie.IMDbRating}\n" +
                $"<b>Watch <a href=\"{watchlink}\" >here</a></b>" +
                $"\n{movie.Image}";
            return Ok(result == null ? "Не знайдено, напишіть назву номрально" : result);
        }
    }
}
