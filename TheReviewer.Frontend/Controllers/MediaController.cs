using Microsoft.AspNetCore.Mvc;
using TheReviewer.Frontend.Consts;
using TheReviewer.Frontend.Models;
using TheReviewer.Logic.Services;

namespace TheReviewer.Frontend.Controllers
{
    public class MediaController : Controller
    {
        private readonly MediaService _mediaService;
        private readonly ReviewService _reviewService;
        private readonly ReviewerService _reviewerService;

        public MediaController(MediaService mediaService, ReviewService reviewService, ReviewerService reviewerService)
        {
            _mediaService = mediaService;
            _reviewService = reviewService;
            _reviewerService = reviewerService;
        }
        
        private IActionResult IndexForType(int mediaTypeId, string viewPath)
        {
            var media = _mediaService.GetByType(mediaTypeId);
            var reviewers = _reviewerService.GetAll();
            var reviews = _reviewService.GetAll();

            return View(viewPath, new MediaViewModel(media, reviewers, reviews, mediaTypeId));
        }

        public IActionResult Films()
        {
            return IndexForType(MediaTypes.FilmTypeId, "~/Views/Film/Index.cshtml");
        }

        public IActionResult Shows()
        {
            return IndexForType(MediaTypes.ShowTypeId, "~/Views/Show/Index.cshtml");
        }

        public IActionResult Games()
        {
            return IndexForType(MediaTypes.GameTypeId, "~/Views/Game/Index.cshtml");
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
                MediaTypes.FilmTypeId => "~/Views/Film/Details.cshtml",
                MediaTypes.GameTypeId => "~/Views/Game/Details.cshtml",
                MediaTypes.ShowTypeId => "~/Views/Show/Details.cshtml",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaTypeId), "Unsupported media type.")
            };
        }
    }
}
