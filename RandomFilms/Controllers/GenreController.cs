using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RandomFilms.Data;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    [Authorize(Roles = "Editor,Moderator,Admin")]
    [Route("Admin/[controller]/[action]")]
    public class GenreController : Controller
    {
        DataManager data;
        public GenreController(DataManager _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return RedirectToAction("AllGenres");
        }
        [HttpGet]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult AddGenre(GenreModel genres)
        {
            if (data.Generes.CheckGenreByName(genres.Genre))
            {
                GenreModel gen = new GenreModel
                {
                    Id = 2
                };
                return View(gen);
            }
            data.Generes.Save(genres);
            GenreModel g = new GenreModel
            {
                Id = 1
            };
            return View(g);
        }
        public IActionResult AllGenres()
        {
            List<FilmGenreTable> table = new List<FilmGenreTable>();
            List<GenreModel> model = data.Generes.AllGenres().ToList();
            model.Sort((x, y) => x.Genre.CompareTo(y.Genre));
            foreach (var item in model)
            {
                int count = data.FilmGenre.CountFilmGenre(item.Genre);
                FilmGenreTable film = new FilmGenreTable
                {
                    Genre = item.Genre,
                    Count = count
                };
                table.Add(film);
            }
            return View(table);
        }
        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult Delete(int id)
        {
            GenreModel genre = data.Generes.GetGenreById(id);
            if (genre != null)
            {
                data.Generes.Delete(genre);
            }
            return RedirectToAction("AllGenres");
        }
    }
}
