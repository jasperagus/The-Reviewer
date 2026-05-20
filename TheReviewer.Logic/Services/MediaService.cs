using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class MediaService(IMediaRepository repository) : IMediaService
{
    public List<MediaModel> GetAll()
    {
        return repository.GetAll().Select(m => new MediaModel(
            m.Id,
            m.Name,
            m.Publisher,
            m.Score,
            m.TypeId,
            m.CreatedAt,
            m.UpdatedAt
        )).ToList();
    }

    public List<MediaModel> GetByType(int typeId)
    {
        return repository.GetByType(typeId).Select(m => new MediaModel(
            m.Id,
            m.Name,
            m.Publisher,
            m.Score,
            m.TypeId,
            m.CreatedAt,
            m.UpdatedAt
        )).ToList();
    }
}