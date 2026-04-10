using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.Repositories;
using TheReviewer.Frontend.Models;
using TheReviewer.Models;
using TheReviewer.Data.DTO;

namespace TheReviewer.Frontend.Controllers
{
    public class GameController : Controller    
    {
        private readonly GameRepository _gameRepository;
        private readonly ReviewRepository _reviewRepository;
        private readonly ReviewerRepository _reviewerRepository;

        public GameController(GameRepository gameRepository, ReviewRepository reviewRepository, ReviewerRepository reviewerRepository)
        {
            _gameRepository = gameRepository;
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
        }
        public IActionResult Index()
        {
            var games = new GameRepository().GetAll();
            var reviewers = new ReviewerRepository().GetAll();
            var reviews = new ReviewRepository().GetAll();

            return View(new GameViewModel(games, reviewers, reviews));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var games = new GameRepository().GetAll();
            var reviewers = new ReviewerRepository().GetAll();

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
                var reviewDTO = new ReviewDTO
                {
                    Content = model.Content,
                    Rating = model.Score,
                    ReviewerId = model.ReviewerId,
                    GameId = model.GameId
                };
                
                _reviewRepository.Add(reviewDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
