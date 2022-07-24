using Microsoft.EntityFrameworkCore;
using RandomFilms.Data.Repositories.Interfaces;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Impliment.EF
{
    public class EFCountry : ICountryRepository
    {
        public AppDbContext context { get; set; }
        public EFCountry(AppDbContext _context)
        {
            context = _context;
        }
        public IEnumerable<CountryModel> AllCountries()
        {
            return context.Countries;
        }

        public void Delete(CountryModel model)
        {
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            CountryModel model = context.Countries.FirstOrDefault(x => x.Id == id);
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public CountryModel GetCountryById(int id)
        {
            return context.Countries.FirstOrDefault(x => x.Id == id);
        }

        public CountryModel GetCountryByName(string name)
        {
            return context.Countries.FirstOrDefault(x => x.Name == name);
        }

        public void Save(CountryModel model)
        {
            if (model.Id == default)
                context.Entry(model).State = EntityState.Added;
            else
                context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
