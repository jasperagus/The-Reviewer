namespace TheReviewer.Data.DTOs
{
    public record GetReviewerDTO(
        int Id,
        string Name,
        string Email,
        string PasswordHash,
        DateOnly Birthdate,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}