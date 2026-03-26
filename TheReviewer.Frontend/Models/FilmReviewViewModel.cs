using TheReviewer.Models;

namespace TheReviewer.Frontend.Models
{
    public class FilmReviewViewModel
    {
       public FilmModel film { get; set; }
       public List<ReviewModel> reviews { get; set; }
    }
}
