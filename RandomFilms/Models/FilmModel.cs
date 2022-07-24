using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class FilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryFilmModelId { get; set; }
        public IEnumerable<CountryFilmModel> Countries { get; set; }
        public string ReleaseDate { get; set; }
        public int FilmGenereModelId { get; set; }
        public IEnumerable<FilmGenersModel> Genre { get; set; }
        public string Age { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string BackGroundImage { get; set; }
        public double IMDB { get; set; }
        public string TrailerURL { get; set; }
    }
}
