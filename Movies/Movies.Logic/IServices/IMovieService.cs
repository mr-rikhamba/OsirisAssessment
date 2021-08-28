using System;
using System.Threading.Tasks;
using Movies.Logic.Models.NytModels;

namespace Movies.Logic.IServices
{
    public interface IMovieService
    {
        Task<NytTimesModel> GetTopPicks();
        Task<NytTimesModel> Search(string searchString);
    }
}
