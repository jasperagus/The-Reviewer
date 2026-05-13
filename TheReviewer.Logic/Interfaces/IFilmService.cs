using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces
{
    public interface IFilmService
    {
        List<FilmModel> GetAll();
    }
}