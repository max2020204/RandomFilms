using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using RandomFilms.Data;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    [ResponseCache(Duration = 604800, Location = ResponseCacheLocation.Client)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DataManager data;
        public HomeController(ILogger<HomeController> logger, DataManager _data)
        {
            _logger = logger;
            data = _data;
        }
        public IActionResult Index()
        {

            try
            {
                List<GenreModel> genres = data.Generes.AllGenres().ToList();
                genres.Sort((x, y) => x.Genre.CompareTo(y.Genre));
                IndexViewModel model = new IndexViewModel
                {
                    Geners = genres
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now}]\nError: {ex.Message}");
                return Error();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
