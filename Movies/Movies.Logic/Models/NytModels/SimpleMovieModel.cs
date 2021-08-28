using System;
namespace Movies.Logic.Models.NytModels
{
    public class SimpleMovieModel
    {
        public string DisplayTitle { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public string ReleaseDate { get; set; }
        public string NytArticleUrl { get; set; }
        public string Rating { get; internal set; }
        public string HeadlineTitle { get; internal set; }
    }
}
