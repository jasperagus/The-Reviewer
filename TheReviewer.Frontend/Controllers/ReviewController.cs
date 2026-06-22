using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Data.DTOs;
using TheReviewer.Frontend.Consts;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Services;

namespace TheReviewer.Frontend.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly MediaService _mediaService;
        private readonly ReviewService _reviewService;
        private readonly AccountService _accountService;

        public ReviewController(
            MediaService mediaService,
            ReviewService reviewService,
            AccountService accountService)
        {
            _mediaService = mediaService;
            _reviewService = reviewService;
            _accountService = accountService;
        }

        public IActionResult CreateFilm()
        {
            return CreateForType(MediaTypes.FilmTypeId);
        }

        public IActionResult CreateGame()
        {
            return CreateForType(MediaTypes.GameTypeId);
        }

        public IActionResult CreateShow()
        {
            return CreateForType(MediaTypes.ShowTypeId);
        }

        public IActionResult CreateSeries()
        {
            return CreateShow();
        }

        private IActionResult CreateForType(int mediaTypeId)
        {
            var reviewerId = GetLoggedInReviewerId();
            if (reviewerId == null) return Challenge();

            var model = new CreateReviewViewModel
            {
                MediaTypeId = mediaTypeId
            };

            PopulateMediaDropdown(model);
            return View(GetCreateViewPath(mediaTypeId), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateReviewViewModel model)
        {
            var reviewerId = GetLoggedInReviewerId();
            if (reviewerId is null)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {
                var reviewDto = new CreateReviewDTO(
                    model.Content,
                    model.Score!.Value,
                    reviewerId.Value,
                    model.MediaId!.Value
                );
                _reviewService.Add(reviewDto);

                return RedirectToAction(GetIndexActionName(model.MediaTypeId), "Media");
            }

            PopulateMediaDropdown(model);
            return View(GetCreateViewPath(model.MediaTypeId), model);
        }

        private void PopulateMediaDropdown(CreateReviewViewModel model)
        {
            model.MediaItems = _mediaService.GetByType(model.MediaTypeId).ConvertAll(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });
        }

        [HttpGet]
        public IActionResult Edit(int id, int mediaTypeId)
        {
            var reviewerId = GetLoggedInReviewerId();
            if (reviewerId == null) return Challenge();

            var review = _reviewService.GetById(id);
            if (review == null) return NotFound();
            if (review.ReviewerId != reviewerId.Value) return Forbid();

            var model = new CreateReviewViewModel
            {
                Id = review.Id,
                Content = review.Content,
                Score = review.Rating,
                MediaId = review.MediaId,
                MediaTypeId = mediaTypeId
            };

            PopulateMediaDropdown(model);

            return View(GetEditViewPath(mediaTypeId), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateReviewViewModel model)
        {
            var reviewerId = GetLoggedInReviewerId();
            if (reviewerId == null) return Challenge();

            var review = _reviewService.GetById(model.Id);
            if (review == null) return NotFound();
            if (review.ReviewerId != reviewerId.Value) return Forbid();

            if (ModelState.IsValid)
            {
                var updateDto = new UpdateReviewDTO(
                    model.Id,
                    model.Content,
                    model.Score!.Value,
                    reviewerId.Value,
                    model.MediaId!.Value
                );

                _reviewService.Update(updateDto);

                return RedirectToAction(GetIndexActionName(model.MediaTypeId), "Media");
            }

            PopulateMediaDropdown(model);

            return View(GetEditViewPath(model.MediaTypeId), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int mediaTypeId)
        {
            var review = _reviewService.GetById(id);
            if (review == null) return NotFound();

            var reviewerId = GetLoggedInReviewerId();
            if (reviewerId == null) return Challenge();
            

            if (review.ReviewerId != reviewerId.Value) return Forbid();
            

            _reviewService.Delete(id);

            return RedirectToAction(GetIndexActionName(mediaTypeId), "Media");
        }

        private string GetCreateViewPath(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                MediaTypes.FilmTypeId => "~/Views/Film/Create.cshtml",
                MediaTypes.GameTypeId => "~/Views/Game/Create.cshtml",
                MediaTypes.ShowTypeId => "~/Views/Show/Create.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }

        private string GetEditViewPath(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                MediaTypes.FilmTypeId => "~/Views/Film/Edit.cshtml",
                MediaTypes.GameTypeId => "~/Views/Game/Edit.cshtml",
                MediaTypes.ShowTypeId => "~/Views/Show/Edit.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }

        private string GetIndexActionName(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                MediaTypes.FilmTypeId => "Films",
                MediaTypes.GameTypeId => "Games",
                MediaTypes.ShowTypeId => "Shows",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }

        private int? GetLoggedInReviewerId()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (!string.IsNullOrWhiteSpace(email))
            {
                return _accountService.GetByEmail(email)?.Id;
            }

            var reviewerIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(reviewerIdValue, out var reviewerId) ? reviewerId : null;
        }
    }
}
