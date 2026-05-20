namespace TheReviewer.Data.DTOs
{
    public record GetReviewDTO(
        int Id,
        string Content,
        int Rating,
        int ReviewerId,
        int? MediaId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}