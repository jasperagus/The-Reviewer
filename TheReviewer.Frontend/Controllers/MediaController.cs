using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.DTOs;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Interfaces;

namespace TheReviewer.Frontend.Controllers
{
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IReviewService _reviewService;
        private readonly IReviewerService _reviewerService;

        // Type IDs
        private const int FilmTypeId = 1;
        private const int GameTypeId = 2;

        public MediaController(IMediaService mediaService, IReviewService reviewService, IReviewerService reviewerService)
        {
            _mediaService = mediaService;
            _reviewService = reviewService;
            _reviewerService = reviewerService;
        }

        public IActionResult Films()
        {
            var media = _mediaService.GetByType(FilmTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View("Index", new MediaViewModel(media, reviewers, reviews, FilmTypeId));
        }

        public IActionResult Games()
        {
            var media = _mediaService.GetByType(GameTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View("Index", new MediaViewModel(media, reviewers, reviews, GameTypeId));
        }

        public IActionResult CreateFilm()
        {
            var media = _mediaService.GetByType(FilmTypeId);
            var reviewers = _reviewerService.GetAll();

            var mediaSelectItems = media.ConvertAll(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });

            var reviewerSelectItems = reviewers.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            return View("Create", new CreateReviewViewModel()
            {
                MediaItems = mediaSelectItems,
                ReviewerItems = reviewerSelectItems,
                MediaTypeId = FilmTypeId
            });
        }

        public IActionResult CreateGame()
        {
            var media = _mediaService.GetByType(GameTypeId);
            var reviewers = _reviewerService.GetAll();

            var mediaSelectItems = media.ConvertAll(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });

            var reviewerSelectItems = reviewers.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            return View("Create", new CreateReviewViewModel()
            {
                MediaItems = mediaSelectItems,
                ReviewerItems = reviewerSelectItems,
                MediaTypeId = GameTypeId
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
                    model.MediaId
                );
                _reviewService.Add(reviewDTO);

                return model.MediaTypeId == FilmTypeId ? RedirectToAction(nameof(Films)) : RedirectToAction(nameof(Games));
            }

            PopulateCreateDropdowns(model);
            return View(model);
        }

        private void PopulateCreateDropdowns(CreateReviewViewModel model)
        {
            var media = _mediaService.GetByType(model.MediaTypeId);
            var reviewers = _reviewerService.GetAll();

            model.MediaItems = media.ConvertAll(m => new SelectListItem()
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });

            model.ReviewerItems = reviewers.ConvertAll(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });
        }
    }
}