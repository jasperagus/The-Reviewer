using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces
{
    public interface IReviewRepository
    {
        List<GetReviewDTO> GetAll();

        void Add(CreateReviewDTO review);
    }
}