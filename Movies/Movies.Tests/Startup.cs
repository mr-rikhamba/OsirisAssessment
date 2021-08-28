using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Logic.IServices;
using Movies.Logic.Models.Configs;
using Movies.Logic.Services;
using Movies.Tests.Helpers;

namespace Movies.Tests
{
    public class Startup
    {

        public Startup()
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfiguration>(sp =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json");
                return configurationBuilder.Build();
            });
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            IConfiguration config = configuration as IConfiguration;
            services.Configure<NytApiKey>(action =>
            {
                var key = config.GetSection("NytApiKey").Get<NytApiKey>();
                action.Key = key.Key;
            });
            services.Configure<EndPoints>(action => {

                var endPoints = config.GetSection("Endpoints").Get<EndPoints>();
                action.BaseApi = endPoints.BaseApi;
                action.Picks = endPoints.Picks;
                action.Search = endPoints.Search;

            });

            services.AddScoped<ICustomHttpClient, MockCustomClient>();
            services.AddScoped<IMovieService, MovieService>();
        }

        public IConfiguration Configuration { get; }
    }
}
