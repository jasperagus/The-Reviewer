namespace TheReviewer.Data.DTOs;

public record GetAccountDTO(
    int Id,
    string Email,
    string PasswordHash,
    string Role,
    DateTime CreatedAt
);
