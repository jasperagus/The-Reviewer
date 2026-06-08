
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheReviewer.Frontend.Models
{
    public class CreateReviewViewModel
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [Required(ErrorMessage = "You must enter a score")]
        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public int? Score { get; set; }
        public int ReviewerId { get; set; }
        [Required(ErrorMessage = "Please select a game.")]
        public int? MediaId { get; set; }
        public int MediaTypeId { get; set; } // 1 = Film, 2 = Game
        public List<SelectListItem> MediaItems { get; set; } = new();
        public List<SelectListItem> ReviewerItems { get; set; } = new();
    }
}