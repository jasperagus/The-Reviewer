using TheReviewer.Data.DTOs;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces
{
    public interface IReviewService
    {
        List<ReviewModel> GetAll();

        ReviewModel? GetById(int id);

        void Add(CreateReviewDTO review);

        void Update(UpdateReviewDTO review);
    }
}