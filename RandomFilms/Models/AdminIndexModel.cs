using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class AdminIndexModel
    {
        public IEnumerable<FilmModel> Films { get; set; }
        public int GenresCount { get; set; }
    }
}
