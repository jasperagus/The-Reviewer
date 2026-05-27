using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces
{
    public interface IReviewRepository
    {
        List<GetReviewDTO> GetAll();
        GetReviewDTO? GetById(int id);

        void Add(CreateReviewDTO review);
        void Update(UpdateReviewDTO review);
    }
}