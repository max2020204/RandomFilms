using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Models
{
    public class IndexViewModel
    {
        public IEnumerable<GenreModel> Geners { get; set; }
        public SelectedCategoriesModel Selected { get; set; }
    }
}
