using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces
{
    public interface IMediaRepository
    {
        List<GetMediaDTO> GetAll();
        List<GetMediaDTO> GetByType(int typeId);
    }
}