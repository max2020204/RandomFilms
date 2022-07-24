using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Interfaces
{
    public interface ICountryFilmRepository
    {
        IEnumerable<CountryFilmModel> AllFilmCountries();
        CountryFilmModel GetCountryFilmById(int id);
        int CountCountries(string country);
        IQueryable<CountryFilmModel> CountriesByFilmId(int id);
        void Save(CountryFilmModel model);
        void Delete(int id);
        void Delete(CountryFilmModel model);
    }
}
