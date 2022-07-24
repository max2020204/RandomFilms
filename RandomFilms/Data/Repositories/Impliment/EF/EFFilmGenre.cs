using Microsoft.EntityFrameworkCore;
using RandomFilms.Data.Repositories.Interfaces;
using RandomFilms.Models;
using System.Collections.Generic;
using System.Linq;

namespace RandomFilms.Data.Repositories.Impliment.EF
{
    public class EFFilmGenre : IFilmGenreRepository
    {
        public AppDbContext context { get; set; }
        public EFFilmGenre(AppDbContext _context)
        {
            context = _context;
        }
        public int CountFilmGenre(string genre)
        {
            return context.FilmGeneres.Where(x => x.Gener == genre).Count();
        }

        public IQueryable<FilmGenersModel> GenresById(int id)
        {
            return context.FilmGeneres.Where(x => x.FilmModelId == id);
        }

        public void Save(FilmGenersModel model)
        {
            if (model.Id == default)
                context.Entry(model).State = EntityState.Added;
            else
                context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            FilmGenersModel item = GetGenreById(id);
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public IEnumerable<FilmGenersModel> AllFilmGenres()
        {
            return context.FilmGeneres;
        }

        public FilmGenersModel GetGenreById(int id)
        {
            return context.FilmGeneres.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(FilmGenersModel model)
        {
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
