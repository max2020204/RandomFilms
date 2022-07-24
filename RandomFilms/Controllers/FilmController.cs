using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomFilms.Data;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    [ResponseCache(Duration = 2678400, Location = ResponseCacheLocation.Client)]
    public class FilmController : Controller
    {
        private readonly ILogger<FilmController> _logger;
        static Random random = new Random();
        DataManager data;
        public FilmController(DataManager _data, ILogger<FilmController> logger)
        {
            data = _data;
            _logger = logger;
        }
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [HttpGet]
        [Route("film/")]
        public IActionResult Index()
        {
            try
            {
                var model = data.Films.GetFilmByFilmId(new Random().Next(1, (data.Films.AllFilms().Count() + 1)));
                if (model == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Index", new { id = model.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now}]\nError: {ex.Message}");
                return NotFound();
            }

        }

        [HttpGet]
        [Route("film/{id}")]
        public IActionResult Index(int id)
        {
            try
            {
                var model = data.Films.GetFilmById(id);
                if (model != null)
                {
                    FilmModelView view = new FilmModelView
                    {
                        Film = model,
                        State = 0
                    };
                    return View(view);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now}]\nError: {ex.Message}");
                return NotFound();

            }
        }
        [HttpGet]
        public IActionResult Index(FilmModelView view)
        {
            if (view.State == 1)
            {
                view.Film = data.Films.GetFilmByFilmId(new Random().Next(1, (data.Films.AllFilms().Count() + 1)));
            }
            return View(view);
        }
        [HttpPost]
        public IActionResult Index(SelectedCategoriesModel model)
        {
            try
            {
                List<GenreModel> allgeners = data.Generes.AllGenres().ToList();
                List<FilmGenersModel> generIn = new List<FilmGenersModel>();
                List<FilmGenersModel> generOut = new List<FilmGenersModel>();
                List<FilmModel> sorted = new List<FilmModel>();
                if (model.GenreIn != null || model.GenreOut != null)
                {
                    if (model.GenreIn != null)
                    {
                        for (int i = 0; i < model.GenreIn.Length; i++)
                        {
                            FilmGenersModel gener = new FilmGenersModel
                            {
                                Id = i,
                                Gener = allgeners.Find(x => x.Id.Equals(model.GenreIn[i])).Genre
                            };
                            generIn.Add(gener);
                        }
                    }
                    if (model.GenreOut != null)
                    {
                        for (int i = 0; i < model.GenreOut.Length; i++)
                        {
                            FilmGenersModel gener = new FilmGenersModel
                            {
                                Id = i,
                                Gener = allgeners.Find(x => x.Id.Equals(model.GenreOut[i])).Genre
                            };
                            generOut.Add(gener);
                        }
                    }
                    if (generIn.Count > 0 && generOut.Count > 0)
                    {
                        sorted = data.Films.GetSelectedGeners(generIn, generOut).ToList();
                    }
                    else if (generIn.Count > 0 && generOut.Count == 0)
                    {
                        sorted = data.Films.GetSelectedGenersIn(generIn).ToList();
                    }
                    else if (generIn.Count == 0 && generOut.Count > 0)
                    {
                        sorted = data.Films.GetSelectedGenersOut(generOut).ToList();
                    }
                    if (sorted.Count == 0)
                    {
                        return Index(new FilmModelView { State = 1 });
                    }
                    int index = random.Next(sorted.Count);
                    var filmmodel = data.Films.GetFilmById(sorted[index].Id);
                    GC.Collect();

                    return RedirectToAction("Index", new { id = filmmodel.Id });
                }
                else
                {
                    var filmModel = data.Films.GetFilmByFilmId(new Random().Next(1, (data.Films.AllFilms().Count() + 1)));
                    if (filmModel == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction("Index", new { id = filmModel.Id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now}]\nError: {ex.Message}");
                return NotFound();
            }
        }
    }
}
