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
    public ReviewModel? GetById(int id)
    {
        var r = repository.GetById(id); // now returns GetReviewDTO?
        if (r == null) return null;
        return new ReviewModel(
            r.Id,
            r.Content,
            r.Rating,
            r.ReviewerId,
            r.MediaId,
            r.CreatedAt,
            r.UpdatedAt
        );
    }

    public void Update(UpdateReviewDTO review)
    {
        repository.Update(review);
    }
}