using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class ReviewerService
{
    private readonly IReviewerRepository _repository;

    public ReviewerService(IReviewerRepository repository)
    {
        _repository = repository;
    }

    public List<ReviewerModel> GetAll()
    {
        return _repository.GetAll().Select(r => new ReviewerModel(
            r.Id,
            r.Name,
            r.Birthdate,
            r.CreatedAt,
            r.UpdatedAt
        )).ToList();
    }
}