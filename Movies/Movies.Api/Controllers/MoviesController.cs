using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Movies.Logic.IServices;
using Movies.Logic.Models.NytModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MoviesController> _logger;
        private const string MovieCacheKey = "MoviePicks";

        public MoviesController(ILogger<MoviesController> logger, IMovieService movieService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _movieService = movieService;
            _memoryCache = memoryCache;
        }

        [HttpGet("picks")]

        public async Task<IActionResult> Get(bool forceRefresh)
        {
            try
            {
                if (forceRefresh)
                {
                    //Remove key from cache if forceRefresh is true
                    _memoryCache.Remove(MovieCacheKey);
                    _logger.LogWarning("Cache Cleared");
                }
                var resultData = await _memoryCache.GetOrCreateAsync(MovieCacheKey, async (ICacheEntry arg) =>
                {
                    var data = await _movieService.GetTopPicks();
                    _logger.LogInformation("Fetching movies");
                    return data;
                });
                return Ok(resultData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest(default);
            }
        }
        [HttpGet("search/{searchString}")]

        public async Task<IActionResult> Get(string searchString)
        {
            try
            {
                var searchResult = await _memoryCache.GetOrCreateAsync(searchString, async (ICacheEntry arg) =>
                {
                    var data = await _movieService.Search(searchString);
                    _logger.LogInformation($"Searched for {searchString}");
                    return data;
                });
                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return BadRequest(default);
            }
        }
    }
}
