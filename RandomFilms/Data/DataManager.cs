using RandomFilms.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Data
{
    public class DataManager
    {
        public IFilmRepository Films { get; set; }
        public IGenereRepository Generes { get; set; }
        public IFilmGenreRepository FilmGenre { get; set; }
        public ICountryRepository Country { get; set; }
        public ICountryFilmRepository CountryFilm { get; set; }
        public DataManager(IFilmRepository _Films, IGenereRepository _Gener, IFilmGenreRepository _FilmGenre, ICountryRepository _country, ICountryFilmRepository _countryFilm)
        {
            Films = _Films;
            Generes = _Gener;
            FilmGenre = _FilmGenre;
            Country = _country;
            CountryFilm = _countryFilm;
        }
    }
}
