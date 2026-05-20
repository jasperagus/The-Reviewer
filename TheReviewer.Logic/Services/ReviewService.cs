using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class ReviewService(IReviewRepository repository) : IReviewService
{
    public List<ReviewModel> GetAll()
    {
        return repository.GetAll().Select(r => new ReviewModel(
            r.Id,
            r.Content,
            r.Rating,
            r.ReviewerId,
            r.MediaId,
            r.CreatedAt,
            r.UpdatedAt
        )).ToList();
    }

    public void Add(CreateReviewDTO review)
    {
        repository.Add(review);
    }
}