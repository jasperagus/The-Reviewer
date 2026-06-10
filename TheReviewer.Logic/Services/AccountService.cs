using System.Security.Cryptography;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class AccountService(IAccountRepository repository) : IAccountService
{
    private const string DefaultRole = "User";

    public CreateAccountResult Create(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        if (!IsValidEmail(normalizedEmail))
        {
            return CreateAccountResult.Failed(CreateAccountError.InvalidEmail);
        }

        if (!IsStrongPassword(password))
        {
            return CreateAccountResult.Failed(CreateAccountError.WeakPassword);
        }

        if (repository.GetByEmail(normalizedEmail) is not null)
        {
            return CreateAccountResult.Failed(CreateAccountError.EmailAlreadyExists);
        }

        var account = repository.Create(new CreateAccountDTO(
            normalizedEmail,
            HashPassword(password),
            DefaultRole,
            DateTime.UtcNow
        ));

        return CreateAccountResult.Created(new AccountModel(
            account.Id,
            account.Email,
            account.Role,
            account.CreatedAt
        ));
    }

    private static bool IsValidEmail(string email)
    {
        return email.Contains('@') && email.Contains('.') && !email.Contains(' ');
    }

    private static bool IsStrongPassword(string password)
    {
        return password.Length >= 8
               && password.Any(char.IsUpper)
               && password.Any(char.IsLower)
               && password.Any(char.IsDigit);
    }

    private static string HashPassword(string password)
    {
        const int saltSize = 16;
        const int keySize = 32;
        const int iterations = 100_000;

        var salt = RandomNumberGenerator.GetBytes(saltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            keySize
        );

        return $"PBKDF2-SHA256.{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
    }
}
