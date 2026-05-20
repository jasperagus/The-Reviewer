using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheReviewer.Frontend.Models
{
    public class CreateReviewViewModel
    {
        public string Content { get; set; }
        public int Score { get; set; }
        public int ReviewerId { get; set; }
        public int? MediaId { get; set; }
        public int MediaTypeId { get; set; } // 1 = Film, 2 = Game
        public List<SelectListItem> MediaItems { get; set; } = new();
        public List<SelectListItem> ReviewerItems { get; set; } = new();
    }
}