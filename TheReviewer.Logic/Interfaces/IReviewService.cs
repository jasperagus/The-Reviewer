using TheReviewer.Data.DTOs;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces
{
    public interface IReviewService
    {
        List<ReviewModel> GetAll();

        void Add(CreateReviewDTO review);
    }
}