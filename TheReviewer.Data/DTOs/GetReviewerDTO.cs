namespace TheReviewer.Data.DTOs
{
    public record GetReviewerDTO(
        int Id,
        string Name,
        DateOnly Birthdate,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}