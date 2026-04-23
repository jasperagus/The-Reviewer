using System.ComponentModel.DataAnnotations;
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
        [Range(1, 100, ErrorMessage = "Score must be between 1 and 100.")]
        public int Score { get; set; }
        [Required(ErrorMessage = "Please select a film.")]
        public int FilmId { get; set; }
        [Required(ErrorMessage = "Please select a game.")]
        public int GameId { get; set; }
        public int ReviewerId { get; set; }
    }
}