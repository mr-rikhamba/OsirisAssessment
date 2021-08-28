using System;
using System.Linq;
using Movies.Logic.IServices;
using Xunit;

namespace Movies.Tests
{
    public class MovieTests
    {
        private readonly IMovieService _movieService;

        public MovieTests(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [Fact]
        public async void CanRetrieveData()
        {
            var data = await _movieService.GetTopPicks();

            Assert.True(data.num_results == 20);
        }
        [Theory]
        [InlineData("Candyman")]
        [InlineData("Candyman: Farewell to the Flesh")] 
        public async void TestMovieTitles(string movieTitle)
        {
            var data = await _movieService.Search(movieTitle);

            Assert.Contains(data.results, c =>c.display_title == movieTitle);
        }
    }
}
