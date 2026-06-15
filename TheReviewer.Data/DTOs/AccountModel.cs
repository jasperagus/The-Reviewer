namespace TheReviewer.Data.DTOs;

public record AccountModel(
    string Email,
    string PasswordHash,
    string Role,
    DateTime CreatedAt
);
