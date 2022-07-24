using Microsoft.EntityFrameworkCore;
using RandomFilms.Data.Repositories.Interfaces;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Impliment.EF
{
    public class EFFilmCountry : ICountryFilmRepository
    {
        public AppDbContext context { get; set; }
        public EFFilmCountry(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<CountryFilmModel> AllFilmCountries()
        {
            return context.FilmCountries;
        }

        public int CountCountries(string country)
        {
            return context.FilmCountries.Where(x => x.Country == country).Count();
        }

        public IQueryable<CountryFilmModel> CountriesByFilmId(int id)
        {
            return context.FilmCountries.Where(x => x.FilmModelId == id);
        }

        public void Delete(int id)
        {
            CountryFilmModel model = GetCountryFilmById(id);
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();

        }

        public void Save(CountryFilmModel model)
        {
            if (model.Id == default)
                context.Entry(model).State = EntityState.Added;
            else
                context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public CountryFilmModel GetCountryFilmById(int id)
        {
            return context.FilmCountries.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(CountryFilmModel model)
        {
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
