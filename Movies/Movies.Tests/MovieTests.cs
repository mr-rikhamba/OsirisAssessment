using System;
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
    }
}
