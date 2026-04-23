using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.DTO;
using TheReviewer.Data.Repositories;
using TheReviewer.Frontend.Models;
using TheReviewer.Models;

namespace TheReviewer.Frontend.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmRepository _filmRepository;
        private readonly ReviewRepository _reviewRepository;
        private readonly ReviewerRepository _reviewerRepository;

        public FilmController(FilmRepository filmRepository, ReviewRepository reviewRepository, ReviewerRepository reviewerRepository)
        {
            _filmRepository = filmRepository;
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
        }

        public IActionResult Index()
        {
            var films = _filmRepository.GetAll();
            var reviewers = _reviewerRepository.GetAll();
            var reviews = _reviewRepository.GetAll();

            return View(new FilmViewModel(films, reviewers, reviews));
        }

        public IActionResult Create()
        {
            var films = _filmRepository.GetAll();
            var reviewers = _reviewerRepository.GetAll();

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
                var reviewDTO = new ReviewDTO
                {
                    Content = model.Content,
                    Rating = model.Score,
                    ReviewerId = model.ReviewerId,
                    FilmId = model.FilmId
                };
                _reviewRepository.Add(reviewDTO);

                return RedirectToAction(nameof(Index));
            }

            PopulateCreateDropdowns(model);
            return View(model);
        }

        private void PopulateCreateDropdowns(CreateReviewViewModel model)
        {
            var films = _filmRepository.GetAll();
            var reviewers = _reviewerRepository.GetAll();

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