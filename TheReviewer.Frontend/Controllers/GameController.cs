using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheReviewer.Data.Repositories;
using TheReviewer.Frontend.Models;
using TheReviewer.Models;

namespace TheReviewer.Frontend.Controllers
{
    public class GameController : Controller    
    {
        public IActionResult Index()
        {
            var games = new GameRepository().GetAll();
            var reviewers = new ReviewerRepository().GetAll();
            var reviews = new ReviewRepository().GetAll();

            return View(new GameViewModel(games, reviewers, reviews));
        }

        public IActionResult Create()
        {
            return View("Create");
        }
    }
}
