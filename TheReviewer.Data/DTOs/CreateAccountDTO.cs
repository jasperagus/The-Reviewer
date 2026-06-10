namespace TheReviewer.Data.DTOs;

public record CreateAccountDTO(
    string Email,
    string PasswordHash,
    string Role,
    DateTime CreatedAt
);
