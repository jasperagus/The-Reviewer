using CreateReviewDTO = TheReviewer.Data.DTOs.CreateReviewDTO;
using UpdateReviewDTO = TheReviewer.Data.DTOs.UpdateReviewDTO;
using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class ReviewService
{
    private readonly IReviewRepository _repository;

    public ReviewService(IReviewRepository repository)
    {
        _repository = repository;
    }

    public List<ReviewModel> GetAll()
    {
        return _repository.GetAll().Select(r => new ReviewModel(
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
        _repository.Add(review);
    }

    public ReviewModel? GetById(int id)
    {
        var r = _repository.GetById(id); 
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
        _repository.Update(review);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}
