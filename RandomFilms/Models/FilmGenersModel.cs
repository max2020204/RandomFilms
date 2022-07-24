using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class FilmGenersModel
    {
        public int Id { get; set; }
        public string Gener { get; set; }
        public int FilmModelId { get; set; }
        public FilmModel Film { get; set; }
    }
}
