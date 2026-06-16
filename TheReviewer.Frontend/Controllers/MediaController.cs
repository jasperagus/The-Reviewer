using Microsoft.AspNetCore.Mvc;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Services;

namespace TheReviewer.Frontend.Controllers
{
    public class MediaController : Controller
    {
        private readonly MediaService _mediaService;
        private readonly ReviewService _reviewService;
        private readonly ReviewerService _reviewerService;

        private const int FilmTypeId = 1;
        private const int GameTypeId = 2;
        private const int ShowTypeId = 3;

        public MediaController(MediaService mediaService, ReviewService reviewService, ReviewerService reviewerService)
        {
            _mediaService = mediaService;
            _reviewService = reviewService;
            _reviewerService = reviewerService;
        }

        public IActionResult Films()
        {
            return IndexForType(FilmTypeId, "~/Views/Film/Index.cshtml");
        }

        public IActionResult Shows()
        {
            return IndexForType(ShowTypeId, "~/Views/Show/Index.cshtml");
        }

        public IActionResult Series()
        {
            return Shows();
        }

        public IActionResult Games()
        {
            return IndexForType(GameTypeId, "~/Views/Game/Index.cshtml");
        }

        private IActionResult IndexForType(int mediaTypeId, string viewPath)
        {
            var media = _mediaService.GetByType(mediaTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View(viewPath, new MediaViewModel(media, reviewers, reviews, mediaTypeId));
        }

        [HttpGet]
        public IActionResult Details(int id, int mediaTypeId)
        {
            var mediaList = _mediaService.GetByType(mediaTypeId);
            var mediaItem = mediaList.FirstOrDefault(m => m.Id == id);
            if (mediaItem == null) return NotFound();

            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll().Where(r => r.MediaId == id).ToList();

            var vm = new MediaViewModel([mediaItem], reviewers, reviews, mediaTypeId);

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
