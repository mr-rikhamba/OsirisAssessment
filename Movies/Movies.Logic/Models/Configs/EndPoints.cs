using System;
namespace Movies.Logic.Models.Configs
{
    public class EndPoints
    {

        /// <summary>
        /// https://api.nytimes.com/svc/movies/v2/reviews/
        /// </summary>
        public string BaseApi { get; set; }

        /// <summary>
        /// picks.json?api-key={0}
        /// </summary>
        public string Picks { get; set; }

        /// <summary>
        /// search.json?api-key={0}&query={1}
        /// </summary>
        public string Search { get; set; }
    }
}
