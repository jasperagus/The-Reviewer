using TheReviewer.Models;

namespace TheReviewer.Frontend.Models
{
    public class GameViewModel
    {
        public List<GameModel> Games { get; private set; }
        public List<ReviewerModel> Reviewers { get; private set; }
        public List<ReviewModel> Reviews { get; init; }

        public GameViewModel(List<GameModel> games, List<ReviewerModel> reviewers, List<ReviewModel> reviews)
        {
            Games = games;
            Reviewers = reviewers;
            Reviews = reviews;
        }
    }
}
