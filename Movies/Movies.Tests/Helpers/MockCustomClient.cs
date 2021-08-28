using System;
using System.IO;
using System.Threading.Tasks;
using Movies.Logic.IServices;
using Newtonsoft.Json;

namespace Movies.Tests.Helpers
{
    public class MockCustomClient:ICustomHttpClient
    {
        public MockCustomClient()
        {
        }

        public async Task<T> GetData<T>(string path)
        {
            string filename = null;
            if (path.ToLower().Contains("picks"))
            {
                 filename = "./MockData/picks.json";
            }
            else
            {
                filename = "./MockData/search.json";
            }


            var fileContent = await File.ReadAllTextAsync(filename);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}
