using System.Security.Cryptography;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Interfaces;
using TheReviewer.Logic.Models;
using AccountModel = TheReviewer.Data.DTOs.AccountModel;

namespace TheReviewer.Logic.Services;

public class AccountService(IAccountRepository repository) : IAccountService
{
    private const string DefaultRole = "User";
    private const int DefaultIterations = 100_000;
    private const int SaltSize = 16;
    private const int KeySize = 16;

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

        var account = repository.Create(new AccountModel(
            normalizedEmail,
            HashPassword(password),
            DefaultRole,
            DateTime.UtcNow
        ));

        return CreateAccountResult.Created(new Models.AccountModel(
            account.Id,
            account.Email,
            DefaultRole,
            account.CreatedAt
        ));
    }

    public Models.AccountModel? Login(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var account = repository.GetByEmail(normalizedEmail);

        if (account is null || !VerifyPassword(password, account.PasswordHash))
        {
            return null;
        }

        return new Models.AccountModel(account.Id, account.Email, DefaultRole, account.CreatedAt);
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
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            DefaultIterations,
            HashAlgorithmName.SHA256,
            KeySize
        );

        return $"PBKDF2-SHA256.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return false;
        }

        var parts = passwordHash.Split('.');
        if (parts.Length is not (3 or 4) || parts[0] != "PBKDF2-SHA256")
        {
            return false;
        }

        try
        {
            var hasLegacyIterations = parts.Length == 4;
            var iterations = hasLegacyIterations && int.TryParse(parts[1], out var parsedIterations)
                ? parsedIterations
                : DefaultIterations;
            var salt = Convert.FromBase64String(hasLegacyIterations ? parts[2] : parts[1]);
            var expectedKey = Convert.FromBase64String(hasLegacyIterations ? parts[3] : parts[2]);
            var actualKey = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedKey.Length
            );

            return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
        }
        catch (FormatException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
