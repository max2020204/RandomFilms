using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class EditFilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Age { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string BackGroundImage { get; set; }
        public double IMDB { get; set; }
        public string TrailerURL { get; set; }
        public int[] GenresSelected { get; set; }
        public int[] CountriesSelected { get; set; }
        public IEnumerable<GenreModel> Geners { get; set; }
        public IEnumerable<CountryModel> Countries { get; set; }
        public int State { get; set; }
    }
}
