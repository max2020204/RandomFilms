using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Interfaces
{
    public interface IGenereRepository
    {
        IEnumerable<GenreModel> AllGenres();
        GenreModel GetGenreById(int id);
        bool CheckGenreByName(string name);
        int GenresCount();
        GenreModel GetGenreByString(string name);
        void Save(GenreModel model);
        void Delete(GenreModel model);
        void Delete(int id);
    }
}
