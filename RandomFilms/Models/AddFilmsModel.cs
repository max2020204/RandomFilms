using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class AddFilmsModel
    {
        public FilmModel Films { get; set; }
        public int[] GenresSelected { get; set; }
        public int[] CountriesSelected { get; set; }
        public IEnumerable<GenreModel> Geners { get; set; }
        public IEnumerable<CountryModel> Countries { get; set; }
        public int Added { get; set; }
    }
}
