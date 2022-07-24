using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<CountryModel> AllCountries();
        CountryModel GetCountryById(int id);
        CountryModel GetCountryByName(string name);
        void Save(CountryModel model);
        void Delete(CountryModel model);
        void Delete(int id);
    }
}
