using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Interfaces
{
    public interface IFilmGenreRepository
    {
        IEnumerable<FilmGenersModel> AllFilmGenres();
        FilmGenersModel GetGenreById(int id); 
        int CountFilmGenre(string genre);
        IQueryable<FilmGenersModel> GenresById(int id);
        void Save(FilmGenersModel model);
        void Delete(int id);
        void Delete(FilmGenersModel model);
    }
}
