using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomFilms.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PathNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        public IActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}
