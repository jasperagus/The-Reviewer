using TheReviewer.Models;

namespace TheReviewer.Frontend.Models
{
    public class FilmViewModel
    {
        public List<FilmModel> Films { get; set; }
        public List<ReviewerModel> Reviewers { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public FilmViewModel(List<FilmModel>films, List<ReviewerModel>reviewers, List<ReviewModel>reviews)
        {
            Films = films;
            Reviewers = reviewers;
            Reviews = reviews;
        }
    }
}
