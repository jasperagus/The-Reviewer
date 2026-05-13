using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Repositories;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Services;

namespace TheReviewer.Frontend.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        private readonly IReviewService _reviewService;
        private readonly IReviewerService _reviewerService;

        public GameController(GameService gameService, IReviewService reviewService, IReviewerService reviewerService)
        {
            _gameService = gameService;
            _reviewService = reviewService;
            _reviewerService = reviewerService;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View(new GameViewModel(games, reviewers, reviews));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var games = _gameService.GetAll();
            var reviewers = _reviewerService.GetAll();

            var gameSelectItems = games.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            var reviewerSelectItems = reviewers.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            return View("Create", new CreateReviewViewModel()
            {
                GameItems = gameSelectItems,
                ReviewerItems = reviewerSelectItems
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reviewDTO = new CreateReviewDTO(
                    model.Content,
                    model.Score,
                    model.ReviewerId,
                    model.FilmId,
                    model.GameId
                );

                _reviewService.Add(reviewDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}