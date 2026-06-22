using TheReviewer.Logic.Enums;

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
    
    public string AddCreateAccountError()
    {
        var message = Error switch
        {
            CreateAccountError.InvalidEmail => "Enter a valid email address",
            CreateAccountError.WeakPassword => "Password must be at least 8 characters and include uppercase, lowercase, and a number",
            CreateAccountError.EmailAlreadyExists => "An account with this email already exists",
            _ => "Could not create account"
        };

        return message;
    }
}
