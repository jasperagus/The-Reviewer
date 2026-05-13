using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class GameService(IGameRepository repository) : IGameService
{
    public List<GameModel> GetAll()
    {
        return repository.GetAll().Select(g => new GameModel(
            g.Id,
            g.Name,
            g.Publisher,
            g.Score,
            g.CreatedAt,
            g.UpdatedAt
        )).ToList();
    }
}