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
    public class MovieController : Controller
    {
        DataManager data;
        public MovieController(DataManager _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return RedirectToAction("AllFilms");
        }
        #region Film
        [Authorize(Roles = "Editor,Moderator,Admin")]
        public IActionResult AddFilm()
        {
            var countries = data.Country.AllCountries().ToList();
            countries.Sort((x, y) => x.Name.CompareTo(y.Name));
            var genres = data.Generes.AllGenres().ToList();
            genres.Sort((x, y) => x.Genre.CompareTo(y.Genre));
            AddFilmsModel film = new AddFilmsModel
            {
                Countries = countries,
                Geners = genres
            };
            return View(film);
        }
        [HttpPost]
        [Authorize(Roles = "Editor,Moderator,Admin")]
        public IActionResult AddFilm(AddFilmsModel film)
        {
            var countries = data.Country.AllCountries().ToList();
            countries.Sort((x, y) => x.Name.CompareTo(y.Name));
            var genres = data.Generes.AllGenres().ToList();
            genres.Sort((x, y) => x.Genre.CompareTo(y.Genre));
            if (data.Films.CheckFilmByName(film.Films.Name))
            {
                film.Added = 3;
                film.Countries = countries;
                film.Geners = genres;
                return View(film);
            }
            try
            {
                int filmid = data.Films.AllFilms().Count();
                string trailer = film.Films.TrailerURL;
                if (film.Films.TrailerURL.Contains("watch?v="))
                {
                    trailer = trailer.Replace("watch?v=", "embed/");
                }
                FilmModel newFilm = new FilmModel
                {
                    Name = film.Films.Name,
                    FilmGenereModelId = filmid + 1,
                    CountryFilmModelId = filmid + 1,
                    ReleaseDate = DateTime.Parse(film.Films.ReleaseDate).ToLongDateString(),
                    Age = film.Films.Age,
                    Time = film.Films.Time,
                    Description = film.Films.Description,
                    IMDB = film.Films.IMDB,
                    TrailerURL = film.Films.TrailerURL,
                    Image = film.Films.Image,
                    BackGroundImage = film.Films.BackGroundImage,
                };
                data.Films.Save(newFilm);
                for (int i = 0; i < film.GenresSelected.Length; i++)
                {
                    FilmGenersModel model = new FilmGenersModel
                    {
                        Id = default,
                        Gener = data.Generes.GetGenreById(film.GenresSelected[i]).Genre,
                        FilmModelId = filmid + 1
                    };
                    data.FilmGenre.Save(model);
                }
                for (int i = 0; i < film.CountriesSelected.Length; i++)
                {
                    CountryFilmModel model = new CountryFilmModel
                    {
                        Id = default,
                        Country = data.Country.GetCountryById(film.CountriesSelected[i]).Name,
                        FilmModelId = filmid + 1
                    };
                    data.CountryFilm.Save(model);
                }
                AddFilmsModel viewmodel = new AddFilmsModel
                {
                    Countries = countries,
                    Geners = genres,
                    Added = 1

                };
                ModelState.Clear();
                return View(viewmodel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                AddFilmsModel viewmodel = new AddFilmsModel
                {
                    Countries = countries,
                    Geners = genres,
                    Added = 2
                };
                return View(viewmodel);
            }
        }
        public IActionResult AllFilms()
        {
            return View(data.Films.AllFilms());
        }
        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult Delete(int id)
        {
            data.Films.Delete(id);
            return RedirectToAction("AllFilms");
        }
        #endregion
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult Edit(int id)
        {
            FilmModel film = data.Films.GetFilmById(id);
            int[] countries = new int[film.Countries.Count()];
            int[] genres = new int[film.Genre.Count()];
            int n = 0;
            if (film == null)
            {
                return NotFound();
            }
            foreach (var item in film.Countries)
            {
                countries[n] = data.Country.GetCountryByName(item.Country).Id;
                n++;
            }
            n = 0;
            foreach (var item in film.Genre)
            {
                genres[n] = data.Generes.GetGenreByString(item.Gener).Id;
                n++;
            }
            EditFilmModel edit = new EditFilmModel
            {
                Id = film.Id,
                Name = film.Name,
                Age = film.Age,
                IMDB = film.IMDB,
                ReleaseDate = DateTime.Parse(film.ReleaseDate),
                Time = film.Time,
                BackGroundImage = film.BackGroundImage,
                Description = film.Description,
                Image = film.Image,
                TrailerURL = film.TrailerURL,
                Geners = data.Generes.AllGenres(),
                Countries = data.Country.AllCountries(),
                CountriesSelected = countries,
                GenresSelected = genres
            };
            return View(edit);
        }
        [HttpPost]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult Edit(EditFilmModel edit)
        {
            if (ModelState.IsValid)
            {
                FilmModel model = data.Films.GetFilmById(edit.Id);
                if (model != null)
                {
                    model.Name = edit.Name;
                    model.Age = edit.Age;
                    model.IMDB = edit.IMDB;
                    model.Time = edit.Time;
                    model.ReleaseDate = edit.ReleaseDate.ToLongDateString();
                    model.BackGroundImage = edit.BackGroundImage;
                    model.Description = edit.Description;
                    model.Image = edit.Image;
                    if (edit.TrailerURL.Contains("watch?v="))
                    {
                        edit.TrailerURL = edit.TrailerURL.Replace("watch?v=", "embed/");
                    }
                    model.TrailerURL = edit.TrailerURL;
                    try
                    {
                        data.Films.Save(model);
                        List<string> countryFilms = model.Countries.Select(x => x.Country).ToList();
                        List<string> selectedcoun = new List<string>();
                        for (int i = 0; i < edit.CountriesSelected.Length; i++)
                        {
                            selectedcoun.Add(data.Country.GetCountryById(edit.CountriesSelected[i]).Name);
                        }
                        List<string> rescoun = selectedcoun.Union(countryFilms).Except(countryFilms.Intersect(selectedcoun)).ToList();
                        if (rescoun.Count > 0)
                        {
                            for (int i = 0; i < rescoun.Count(); i++)
                            {
                                if (countryFilms.Contains(rescoun[i]))
                                {
                                    data.CountryFilm.Delete(data.CountryFilm.CountriesByFilmId(model.CountryFilmModelId).FirstOrDefault(x => x.Country == rescoun[i]));
                                }
                                else
                                {
                                    CountryFilmModel newcountry = new CountryFilmModel
                                    {
                                        Id = default,
                                        Country = rescoun[i],
                                        FilmModelId = model.CountryFilmModelId
                                    };
                                    data.CountryFilm.Save(newcountry);
                                }
                            }
                        }
                        List<string> genreFilms = model.Genre.Select(x => x.Gener).ToList();
                        List<string> selectedgenr = new List<string>();
                        for (int i = 0; i < edit.GenresSelected.Length; i++)
                        {
                            selectedgenr.Add(data.Generes.GetGenreById(edit.GenresSelected[i]).Genre);
                        }
                        List<string> resultgenre = selectedgenr.Union(genreFilms).Except(genreFilms.Intersect(selectedgenr)).ToList();
                        if (resultgenre.Count > 0)
                        {
                            for (int i = 0; i < resultgenre.Count(); i++)
                            {
                                if (genreFilms.Contains(resultgenre[i]))
                                {
                                    data.FilmGenre.Delete(data.FilmGenre.GenresById(model.FilmGenereModelId).FirstOrDefault(x => x.Gener == resultgenre[i]));
                                }
                                else
                                {
                                    FilmGenersModel newgenre = new FilmGenersModel
                                    {
                                        Id = default,
                                        Gener = resultgenre[i],
                                        FilmModelId = model.FilmGenereModelId
                                    };
                                    data.FilmGenre.Save(newgenre);
                                }
                            }
                        }
                        edit.State = 1;
                        edit.Geners = data.Generes.AllGenres();
                        edit.Countries = data.Country.AllCountries();
                        return View(edit);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        edit.State = 2;
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Check the fields");
                edit.State = 2;
            }

            return View(edit);
        }
        [HttpPost]
        [Authorize(Roles = "Editor,Moderator,Admin")]
        public IActionResult Preview(AddFilmsModel film)
        {
            List<CountryFilmModel> countryModels = new List<CountryFilmModel>();
            List<FilmGenersModel> filmGeners = new List<FilmGenersModel>();
            for (int i = 0; i < film.CountriesSelected.Length; i++)
            {
                CountryFilmModel model = new CountryFilmModel
                {
                    Country = data.Country.GetCountryById(film.CountriesSelected[i]).Name
                };
                countryModels.Add(model);
            }
            for (int i = 0; i < film.GenresSelected.Length; i++)
            {
                FilmGenersModel model = new FilmGenersModel
                {
                    Gener = data.Generes.GetGenreById(film.GenresSelected[i]).Genre
                };
                filmGeners.Add(model);
            }
            string trailer = film.Films.TrailerURL;
            if (film.Films.TrailerURL.Contains("watch?v="))
            {
                trailer = trailer.Replace("watch?v=", "embed/");
            }
            FilmModel newFilm = new FilmModel
            {
                Name = film.Films.Name,
                Countries = countryModels,
                ReleaseDate = DateTime.Parse(film.Films.ReleaseDate).ToLongDateString(),
                Genre = filmGeners,
                Age = film.Films.Age,
                Time = film.Films.Time,
                Description = film.Films.Description,
                IMDB = film.Films.IMDB,
                TrailerURL = film.Films.TrailerURL,
                Image = film.Films.Image,
                BackGroundImage = film.Films.BackGroundImage,
            };
            FilmModelView view = new FilmModelView
            {
                Film = newFilm,
                State = 10
            };
            return View("~/Views/Film/Index.cshtml", view);
        }
    }
}
