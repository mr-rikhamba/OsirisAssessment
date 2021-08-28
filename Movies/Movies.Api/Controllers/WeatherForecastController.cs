using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Movies.Logic.IServices;
using Movies.Logic.Models.NytModels;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IMovieService _movieService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMovieService movieService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _movieService = movieService;
            _memoryCache = memoryCache;
        }

        [HttpGet]

        public async Task<NytTimesModel> Get()
        {
            return await _memoryCache.GetOrCreateAsync("MoviePicks", async (ICacheEntry arg) =>
           {
               var data = await _movieService.GetTopPicks();
               return data;
           });
        }
    }
}
