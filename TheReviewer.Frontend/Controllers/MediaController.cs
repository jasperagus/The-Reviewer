using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Frontend.Controllers
{
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IReviewService _reviewService;
        private readonly IReviewerService _reviewerService;

        private const int FilmTypeId = 1;
        private const int GameTypeId = 2;
        private const int ShowTypeId = 3;

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

            return View("~/Views/Film/Index.cshtml", new MediaViewModel(media, reviewers, reviews, FilmTypeId));
        }

        public IActionResult Shows()
        {
            return Series();
        }

        public IActionResult Series()
        {
            var media = _mediaService.GetByType(ShowTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View("~/Views/Show/Index.cshtml", new MediaViewModel(media, reviewers, reviews, ShowTypeId));
        }

        public IActionResult Games()
        {
            var media = _mediaService.GetByType(GameTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View("~/Views/Game/Index.cshtml", new MediaViewModel(media, reviewers, reviews, GameTypeId));
        }

        [HttpGet]
        public IActionResult Details(int id, int mediaTypeId)
        {
            var mediaList = _mediaService.GetByType(mediaTypeId);
            var mediaItem = mediaList.FirstOrDefault(m => m.Id == id);
            if (mediaItem == null) return NotFound();

            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll().Where(r => r.MediaId == id).ToList();

            var vm = new MediaViewModel(new List<MediaModel> { mediaItem }, reviewers, reviews, mediaTypeId);

            return View(GetDetailsViewPath(mediaTypeId), vm);
        }

        private string GetDetailsViewPath(int mediaTypeId)
        {
            return mediaTypeId switch
            {
                FilmTypeId => "~/Views/Film/Details.cshtml",
                GameTypeId => "~/Views/Game/Details.cshtml",
                ShowTypeId => "~/Views/Show/Details.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }
    }
}