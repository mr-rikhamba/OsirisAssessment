using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Movies.Logic.IServices;
using Movies.Logic.Models.Configs;
using Movies.Logic.Models.NytModels;
using Newtonsoft.Json;

namespace Movies.Logic.Services
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly EndPoints _endpoints;
        private readonly NytApiKey _key;

        public CustomHttpClient(HttpClient httpClient, IOptions<EndPoints> endpointsOptions, IOptions<NytApiKey> keyOptions)
        {
            _httpClient = httpClient;
            _endpoints = endpointsOptions.Value;
            _httpClient.BaseAddress = new Uri(_endpoints.BaseApi);
        }

        public async Task<T> GetData<T>(string path)
        {
            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            return default;
        }
    }
}
