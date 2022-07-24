using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RandomFilms.Data.Repositories.Interfaces;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Impliment.EF
{
    public class EFFilm : IFilmRepository
    {
        public AppDbContext context { get; set; }
        public EFFilm(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<FilmModel> AllFilms()
        {
            return context.Films.Include(x => x.Countries).Include(x => x.Genre);
        }

        public void Delete(int id)
        {
            FilmModel model = context.Films.FirstOrDefault(x => x.Id == id);
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Delete(FilmModel model)
        {
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public FilmModel GetFilmById(int id)
        {
            return context.Films.Include(x => x.Genre).Include(x => x.Countries).FirstOrDefault(x => x.Id == id);
        }
        public FilmModel GetFilmByFilmId(int id)
        {
            return context.Films.Include(x => x.Genre).FirstOrDefault(x => x.FilmGenereModelId == id);
        }
        public IQueryable<FilmModel> GetFilmsByCountry(string country)
        {
            return context.Films.Include(x => x.Countries).Where(x => x.Countries.Any(o => o.Country == country));
        }

        public IQueryable<FilmModel> GetFilmsByIMDB(double imdb)
        {
            return context.Films.Where(x => x.IMDB == imdb);
        }

        public void Save(FilmModel model)
        {
            if (model.Id == default)
                context.Entry(model).State = EntityState.Added;
            else
                context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }
        public IQueryable<FilmModel> GetSelectedGeners(List<FilmGenersModel> genereIn, List<FilmGenersModel> genereOut)
        {
            return context.Films.Include(c => c.Genre).Where(x => genereIn.Select(o => o.Gener).Any(u => x.Genre.Select(o => o.Gener).Contains(u)) && !genereOut.Select(o => o.Gener).Any(u => x.Genre.Select(o => o.Gener).Contains(u)));
        }

        public IQueryable<FilmModel> GetSelectedGenersIn(List<FilmGenersModel> genereIn)
        {
            return context.Films.Include(c => c.Genre).Where(x => genereIn.Select(o => o.Gener).Any(u => x.Genre.Select(o => o.Gener).Contains(u)));
        }

        public IQueryable<FilmModel> GetSelectedGenersOut(List<FilmGenersModel> genereOut)
        {
            return context.Films.Include(c => c.Genre).Where(x => !genereOut.Select(o => o.Gener).Any(u => x.Genre.Select(o => o.Gener).Contains(u)));
        }

        public bool CheckFilmByName(string name)
        {
            if (context.Films.Where(x => x.Name == name).Count() == 0)
            {
                return false;
            }
            return true;
        }

        public FilmModel GetFilmByName(string name)
        {
            return context.Films.FirstOrDefault(x => x.Name == name);
        }
    }
}
