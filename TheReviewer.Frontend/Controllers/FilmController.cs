using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheReviewer.Data.Repositories;
using TheReviewer.Frontend.Models;
using TheReviewer.Models;

namespace TheReviewer.Frontend.Controllers
{
    public class FilmController : Controller
    {
        public IActionResult Index()
        {
            var repository = new FilmRepository();
            var films = repository.GetAll();
            var reviewers = new ReviewerRepository().GetAll();
            var reviews = new ReviewRepository().GetAll();

            return View(new FilmViewModel(films, reviewers, reviews));

        }
        public IActionResult Create()
        {
            return View("Create");
        }
    }
}
