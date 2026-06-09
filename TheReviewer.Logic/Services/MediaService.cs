using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class MediaService
{
    private readonly IMediaRepository _repository;

    public MediaService(IMediaRepository repository)
    {
        _repository = repository;
    }

    public List<MediaModel> GetAll()
    {
        return _repository.GetAll().Select(m => new MediaModel(
            m.Id,
            m.Name,
            m.Publisher,
            m.Score,
            m.TypeId,
            m.Episodes,
            m.CreatedAt,
            m.UpdatedAt
        )).ToList();
    }

    public List<MediaModel> GetByType(int typeId)
    {
        return _repository.GetByType(typeId).Select(m => new MediaModel(
            m.Id,
            m.Name,
            m.Publisher,
            m.Score,
            m.TypeId,
            m.Episodes,
            m.CreatedAt,
            m.UpdatedAt
        )).ToList();
    }
}