using System.Security.Cryptography;
using TheReviewer.Data.Interfaces;
using TheReviewer.Logic.Enums;
using TheReviewer.Logic.Models;

namespace TheReviewer.Logic.Services;

public class ReviewerService
{
    private readonly IReviewerRepository _repository;
    private const int DefaultIterations = 100_000;
    private const int SaltSize = 16;
    private const int KeySize = 16;

    public ReviewerService(IReviewerRepository repository)
    {
        _repository = repository;
    }

    public List<ReviewerModel> GetAll()
    {
        return _repository.GetAll().Select(r => new ReviewerModel(
            r.Id,
            r.Name,
            r.Email,
            r.PasswordHash,
            r.Birthdate,
            r.CreatedAt,
            r.UpdatedAt
        )).ToList();
    }

    public CreateReviewerResult Create(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        if (!IsValidEmail(normalizedEmail))
        {
            return CreateReviewerResult.Failed(CreateReviewerError.InvalidEmail);
        }

        if (!IsStrongPassword(password))
        {
            return CreateReviewerResult.Failed(CreateReviewerError.WeakPassword);
        }

        if (_repository.GetByEmail(normalizedEmail) is not null)
        {
            return CreateReviewerResult.Failed(CreateReviewerError.EmailAlreadyExists);
        }

        var reviewer = _repository.Create(new TheReviewer.Data.DTOs.CreateReviewerDTO(
            GetDefaultReviewerName(normalizedEmail),
            normalizedEmail,
            HashPassword(password),
            DateTime.UtcNow
        ));

        return CreateReviewerResult.Created(new ReviewerModel(
            reviewer.Id,
            reviewer.Name,
            reviewer.Email,
            reviewer.PasswordHash,
            reviewer.Birthdate,
            reviewer.CreatedAt,
            reviewer.UpdatedAt
        ));
    }

    public ReviewerModel? GetByEmail(string email)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var reviewer = _repository.GetByEmail(normalizedEmail);

        return reviewer is null
            ? null
            : new ReviewerModel(reviewer.Id, reviewer.Name, reviewer.Email, reviewer.PasswordHash, reviewer.Birthdate, reviewer.CreatedAt, reviewer.UpdatedAt);
    }

    public ReviewerModel? Login(string email, string password)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        var reviewer = _repository.GetByEmail(normalizedEmail);

        if (reviewer is null || !VerifyPassword(password, reviewer.PasswordHash))
        {
            return null;
        }

        return new ReviewerModel(reviewer.Id, reviewer.Name, reviewer.Email, reviewer.PasswordHash, reviewer.Birthdate, reviewer.CreatedAt, reviewer.UpdatedAt);
    }

    private static string GetDefaultReviewerName(string email)
    {
        var atIndex = email.IndexOf('@');
        return atIndex > 0 ? email[..atIndex] : email;
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
