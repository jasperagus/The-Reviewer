using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces
{
    public interface IGameRepository
    {
        List<GetGameDTO> GetAll();
    }
}