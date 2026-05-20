using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces
{
    public interface IMediaService
    {
        List<MediaModel> GetAll();
        List<MediaModel> GetByType(int typeId);
    }
}