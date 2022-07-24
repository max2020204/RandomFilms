using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Interfaces
{
    public interface IFilmRepository
    {
        IEnumerable<FilmModel> AllFilms();
        IQueryable<FilmModel> GetFilmsByCountry(string country);
        IQueryable<FilmModel> GetFilmsByIMDB(double imdb);
        IQueryable<FilmModel> GetSelectedGeners(List<FilmGenersModel> genereIn, List<FilmGenersModel> genereOut);
        IQueryable<FilmModel> GetSelectedGenersIn(List<FilmGenersModel> genereIn);
        IQueryable<FilmModel> GetSelectedGenersOut(List<FilmGenersModel> genereOut);
        bool CheckFilmByName(string name);
        FilmModel GetFilmByName(string name);
        FilmModel GetFilmById(int id);
        FilmModel GetFilmByFilmId(int id);
        void Save(FilmModel model);
        void Delete(int id);
        void Delete(FilmModel model);
    }
}
