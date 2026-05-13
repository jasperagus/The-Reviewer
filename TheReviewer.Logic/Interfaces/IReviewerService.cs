using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces
{
    public interface IReviewerService
    {
        List<ReviewerModel> GetAll();
    }
}