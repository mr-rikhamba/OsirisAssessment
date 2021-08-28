using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Logic.Models.NytModels;

namespace Movies.Logic.IServices
{
    public interface IMovieService
    {
        Task<IEnumerable<SimpleMovieModel>> GetTopPicks();
        Task<IEnumerable<SimpleMovieModel>> Search(string searchString);
    }
}
