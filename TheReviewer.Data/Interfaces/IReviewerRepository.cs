using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces
{
    public interface IReviewerRepository
    {
        List<GetReviewerDTO> GetAll();
    }
}