using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class ReviewerService(IReviewerRepository repository) : IReviewerService
{
    public List<ReviewerModel> GetAll()
    {
        return repository.GetAll().Select(r => new ReviewerModel(
            r.Id,
            r.Name,
            r.Birthdate,
            r.CreatedAt,
            r.UpdatedAt
        )).ToList();
    }
}