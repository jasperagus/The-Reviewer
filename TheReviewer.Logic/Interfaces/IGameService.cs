using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces
{
    public interface IGameService
    {
        List<GameModel> GetAll();
    }
}