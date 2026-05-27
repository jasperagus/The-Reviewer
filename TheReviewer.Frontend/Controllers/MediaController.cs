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

            return View("~/Views/Film/Index.cshtml", new MediaViewModel(media, reviewers, reviews, FilmTypeId));        }

        public IActionResult Games()
        {
            var media = _mediaService.GetByType(GameTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View("~/Views/Game/Index.cshtml", new MediaViewModel(media, reviewers, reviews, GameTypeId));        }

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

            return View("~/Views/Film/Create.cshtml", new CreateReviewViewModel()
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

            return View("~/Views/Game/Create.cshtml", new CreateReviewViewModel()
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
        // GET: Media/Edit/5?mediaTypeId=1
        [HttpGet]
        public IActionResult Edit(int id, int mediaTypeId)
        {
            // need service method GetById(int id)
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

            // populate dropdowns for the edit page
            PopulateCreateDropdowns(model);

            // choose view path based on mediaTypeId (1=Film, 2=Game)
            return mediaTypeId == FilmTypeId
                ? View("~/Views/Film/Edit.cshtml", model)
                : View("~/Views/Game/Edit.cshtml", model);
        }

// POST: Media/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                // You will need an Update method and DTO in the logic/data layer.
                var updateDto = new UpdateReviewDTO(
                    model.Id,
                    model.Content,
                    model.Score,
                    model.ReviewerId,
                    model.MediaId ?? 0
                );

                _reviewService.Update(updateDto);

                return model.MediaTypeId == FilmTypeId ? RedirectToAction(nameof(Films)) : RedirectToAction(nameof(Games));
            }

            // if validation failed, re-populate dropdowns and redisplay
            PopulateCreateDropdowns(model);

            return model.MediaTypeId == FilmTypeId
                ? View("~/Views/Film/Edit.cshtml", model)
                : View("~/Views/Game/Edit.cshtml", model);
        }
    }
}