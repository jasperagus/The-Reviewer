using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.DTOs;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Services;

namespace TheReviewer.Frontend.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MediaService _mediaService;
        private readonly ReviewService _reviewService;
        private readonly ReviewerService _reviewerService;

        private const int FilmTypeId = 1;
        private const int GameTypeId = 2;
        private const int ShowTypeId = 3;

        public ReviewController(MediaService mediaService, ReviewService reviewService, ReviewerService reviewerService)
        {
            _mediaService = mediaService;
            _reviewService = reviewService;
            _reviewerService = reviewerService;
        }

        public IActionResult CreateFilm()
        {
            return CreateForType(FilmTypeId);
        }

        public IActionResult CreateGame()
        {
            return CreateForType(GameTypeId);
        }

        public IActionResult CreateShow()
        {
            return CreateForType(ShowTypeId);
        }

        public IActionResult CreateSeries()
        {
            return CreateShow();
        }

        private IActionResult CreateForType(int mediaTypeId)
        {
            var model = new CreateReviewViewModel
            {
                MediaTypeId = mediaTypeId
            };

            PopulateCreateDropdowns(model);
            return View(GetCreateViewPath(mediaTypeId), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reviewDto = new CreateReviewDTO(
                    model.Content,
                    model.Score!.Value,
                    model.ReviewerId,
                    model.MediaId
                );
                _reviewService.Add(reviewDto);

                return RedirectToAction(GetIndexActionName(model.MediaTypeId), "Media");
            }

            PopulateCreateDropdowns(model);
            return View(GetCreateViewPath(model.MediaTypeId), model);
        }

        private void PopulateCreateDropdowns(CreateReviewViewModel model)
        {
            model.MediaItems = _mediaService.GetByType(model.MediaTypeId).ConvertAll(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });

            model.ReviewerItems = _reviewerService.GetAll().ConvertAll(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });
        }

        [HttpGet]
        public IActionResult Edit(int id, int mediaTypeId)
        {
            var review = _reviewService.GetById(id);
            if (review == null) return NotFound();

            var model = new CreateReviewViewModel
            {
                Id = review.Id,
                Content = review.Content,
                Score = review.Rating,
                ReviewerId = review.ReviewerId,
                MediaId = review.MediaId,
                MediaTypeId = mediaTypeId
            };

            PopulateCreateDropdowns(model);

            return View(GetEditViewPath(mediaTypeId), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var updateDto = new UpdateReviewDTO(
                    model.Id,
                    model.Content,
                    model.Score!.Value,
                    model.ReviewerId,
                    model.MediaId ?? 0
                );

                _reviewService.Update(updateDto);

                return RedirectToAction(GetIndexActionName(model.MediaTypeId), "Media");
            }

            PopulateCreateDropdowns(model);

            return View(GetEditViewPath(model.MediaTypeId), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int mediaTypeId)
        {
            var review = _reviewService.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewService.Delete(id);

            return RedirectToAction(GetIndexActionName(mediaTypeId), "Media");
        }

        private string GetCreateViewPath(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                FilmTypeId => "~/Views/Film/Create.cshtml",
                GameTypeId => "~/Views/Game/Create.cshtml",
                ShowTypeId => "~/Views/Show/Create.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }

        private string GetEditViewPath(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                FilmTypeId => "~/Views/Film/Edit.cshtml",
                GameTypeId => "~/Views/Game/Edit.cshtml",
                ShowTypeId => "~/Views/Show/Edit.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }

        private string GetIndexActionName(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                FilmTypeId => "Films",
                GameTypeId => "Games",
                ShowTypeId => "Shows",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }
    }
}
