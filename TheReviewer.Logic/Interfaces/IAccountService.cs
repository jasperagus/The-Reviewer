using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Interfaces;

public interface IAccountService
{
    CreateAccountResult Create(string email, string password);
}
