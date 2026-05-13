using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.DTOs;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Interfaces;

namespace TheReviewer.Frontend.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IReviewService _reviewService;
        private readonly IReviewerService _reviewerService;

        public FilmController(IFilmService filmService, IReviewService reviewService, IReviewerService reviewerService)
        {
            _filmService = filmService;
            _reviewService = reviewService;
            _reviewerService = reviewerService;
        }

        public IActionResult Index()
        {
            var films = _filmService.GetAll();
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View(new FilmViewModel(films, reviewers, reviews));
        }

        public IActionResult Create()
        {
            var films = _filmService.GetAll();
            var reviewers = _reviewerService.GetAll();

            var filmSelectItems = films.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            var reviewerSelectItems = reviewers.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            return View(new CreateReviewViewModel()
            {
                FilmItems = filmSelectItems,
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

            PopulateCreateDropdowns(model);
            return View(model);
        }

        private void PopulateCreateDropdowns(CreateReviewViewModel model)
        {
            var films = _filmService.GetAll();
            var reviewers = _reviewerService.GetAll();

            model.FilmItems = films.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            model.ReviewerItems = reviewers.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });
        }
    }
}