using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Movies.Logic.IServices;
using Movies.Logic.Models.Configs;
using Movies.Logic.Models.NytModels;

namespace Movies.Logic.Services
{
    public class MovieService : IMovieService
    {
        private readonly EndPoints _endpoints;
        private readonly NytApiKey _key;
        private readonly ICustomHttpClient _httpClient;
        public MovieService(IOptions<EndPoints> endpointsOptions, IOptions<NytApiKey> keyOptions, ICustomHttpClient httpClient)
        {
            _endpoints = endpointsOptions.Value;
            _key = keyOptions.Value;
            _httpClient = httpClient;
        }
        public async Task<NytTimesModel> GetTopPicks()
        {
            var path = string.Format(_endpoints.Picks, _key.Key);
            var data = await _httpClient.GetData<NytTimesModel>(path);
            return data;
        }

        public async Task<NytTimesModel> Search(string searchString)
        {
            var path = string.Format(_endpoints.Search, _key.Key, searchString);
            var data = await _httpClient.GetData<NytTimesModel>(path);
            return data;
        }
    }
}
