using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces
{
    public interface IFilmRepository
    {
        List<GetFilmDTO> GetAll();
    }
}