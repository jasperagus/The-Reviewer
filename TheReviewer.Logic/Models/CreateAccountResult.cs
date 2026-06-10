namespace TheReviewer.Logic.Models;

public class CreateAccountResult
{
    public bool Success => Account is not null;
    public AccountModel? Account { get; }
    public CreateAccountError? Error { get; }

    private CreateAccountResult(AccountModel? account, CreateAccountError? error)
    {
        Account = account;
        Error = error;
    }

    public static CreateAccountResult Created(AccountModel account)
    {
        return new CreateAccountResult(account, null);
    }

    public static CreateAccountResult Failed(CreateAccountError error)
    {
        return new CreateAccountResult(null, error);
    }
}
