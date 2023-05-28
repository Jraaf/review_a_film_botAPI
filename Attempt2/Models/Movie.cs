using System.Xml;

namespace Attempt2.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IMDbRating { get; set; }
        public string Stars { get; set; }
        public string Genres { get; set; }
    }
}
