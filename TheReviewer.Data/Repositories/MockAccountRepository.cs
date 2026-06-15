using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories;

public class MockAccountRepository : IAccountRepository
{
    private readonly List<GetAccountDTO> _accounts = [];
    private int _currentId = 1;

    public GetAccountDTO? GetByEmail(string email)
    {
        return _accounts.FirstOrDefault(account =>
            string.Equals(account.Email, email, StringComparison.OrdinalIgnoreCase));
    }

    public GetAccountDTO Create(AccountModel account)
    {
        var savedAccount = new GetAccountDTO(
            _currentId++,
            account.Email,
            account.PasswordHash,
            account.CreatedAt,
            null
        );

        _accounts.Add(savedAccount);

        return savedAccount;
    }
}
