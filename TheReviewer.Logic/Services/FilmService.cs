using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class FilmService(IFilmRepository repository) : IFilmService
{
    public List<FilmModel> GetAll()
    {
        return repository.GetAll().Select(f => new FilmModel(
            f.Id,
            f.Name,
            f.Publisher,
            f.Score,
            f.CreatedAt,
            f.UpdatedAt
        )).ToList();
    }
}