using System;
using System.Threading.Tasks;

namespace Movies.Logic.IServices
{
    public interface ICustomHttpClient
    {
        Task<T> GetData<T>(string path);
    }
}
