using Microsoft.AspNetCore.Mvc.Rendering;
using TheReviewer.Models;

namespace TheReviewer.Frontend.Models
{
    public class CreateReviewViewModel
    {
        public IEnumerable<SelectListItem>? FilmItems { get; set; }
        public IEnumerable<SelectListItem>? GameItems { get; set; } 
        public IEnumerable<SelectListItem>? ReviewerItems { get; set; }
        
        public string Content { get; set; }
        public int Score { get; set; }
        public int FilmId { get; set; }
        public int GameId { get; set; }
        public int ReviewerId { get; set; }
    }
}