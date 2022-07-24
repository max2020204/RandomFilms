using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomFilms.Data;
using RandomFilms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    [Authorize(Roles = "User,Editor,Moderator,Admin")]
    public class AdminController : Controller
    {
        DataManager data;
        public AdminController(DataManager _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return View(new AdminIndexModel
            {
                Films = data.Films.AllFilms(),
                GenresCount = data.Generes.GenresCount()
            });
        }

    }
}
