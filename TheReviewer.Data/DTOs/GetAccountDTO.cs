namespace TheReviewer.Data.DTOs;

public record GetAccountDTO(
    int Id,
    string Email,
    string PasswordHash,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
