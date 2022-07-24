using Microsoft.EntityFrameworkCore;
using RandomFilms.Data.Repositories.Interfaces;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Impliment.EF
{
    public class EFGenre : IGenereRepository
    {
        public AppDbContext context { get; set; }
        public EFGenre(AppDbContext _context)
        {
            context = _context;
        }
        public void Delete(GenreModel model)
        {
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            GenreModel model = context.Genres.FirstOrDefault(x => x.Id == id);
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public IEnumerable<GenreModel> AllGenres()
        {
            return context.Genres;
        }

        public GenreModel GetGenreById(int id)
        {
            return context.Genres.FirstOrDefault(x => x.Id == id);
        }

        public GenreModel GetGenreByString(string name)
        {
            return context.Genres.FirstOrDefault(x => x.Genre == name);
        }

        public void Save(GenreModel model)
        {
            if (model.Id == default)
                context.Entry(model).State = EntityState.Added;
            else
                context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public int GenresCount()
        {
            return context.Genres.Count();
        }

        public bool CheckGenreByName(string name)
        {
            if (context.Genres.Where(x => x.Genre == name).Count() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
