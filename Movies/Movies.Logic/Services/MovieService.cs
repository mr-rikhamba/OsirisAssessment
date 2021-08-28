using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Movies.Logic.IServices;
using Movies.Logic.Models.Configs;
using Movies.Logic.Models.NytModels;
using Microsoft.Extensions.Logging;

namespace Movies.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly EndPoints _endpoints;
        private readonly KeyModel _key;
        private readonly ICustomHttpClient _httpClient;
        private readonly ILogger<MovieService> _logger;
        public MovieService(ILogger<MovieService> logger, IOptions<EndPoints> endpointsOptions, IOptions<KeyModel> keyOptions, ICustomHttpClient httpClient)
        {
            _endpoints = endpointsOptions.Value;
            _key = keyOptions.Value;
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IEnumerable<SimpleMovieModel>> GetTopPicks()
        {
            try
            {
                var path = string.Format(_endpoints.Picks, _key.Key);
                var data = await _httpClient.GetData<NytTimesModel>(path);
                _logger.LogInformation("Fetched Data");
                return ToSimplyModel(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while getting top picks.");
                return default;
            }
        }

        public async Task<IEnumerable<SimpleMovieModel>> Search(string searchString)
        {
            try
            {
                var path = string.Format(_endpoints.Search, _key.Key, searchString);
                var data = await _httpClient.GetData<NytTimesModel>(path);
                _logger.LogInformation("Fetched Data");
                return ToSimplyModel(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while searching for {searchString}.");
                return default;
            }
        }

        private IEnumerable<SimpleMovieModel> ToSimplyModel(NytTimesModel nytTimesModel)
        {

            _logger.LogInformation("Simplifying Data");
            foreach (var movieItem in nytTimesModel.results)
            {
                yield return new SimpleMovieModel
                {
                    DisplayTitle = movieItem.display_title,
                    ImageUrl = movieItem.multimedia?.src ?? "https://picsum.photos/200/300",
                    NytArticleUrl = movieItem.link?.url ,
                    Summary = movieItem.summary_short,
                    ReleaseDate = movieItem.opening_date ?? "TBC",
                    Rating = movieItem.mpaa_rating,
                    HeadlineTitle = movieItem.headline
                };
            }

        }
    }
}
