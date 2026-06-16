using TheReviewer.Data.DTOs;

namespace TheReviewer.Data.Interfaces;

public interface IAccountRepository
{
    GetAccountDTO? GetByEmail(string email);
    GetAccountDTO Create(CreateAccountDTO account);
}
