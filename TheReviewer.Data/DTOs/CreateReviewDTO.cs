namespace TheReviewer.Data.DTOs
{
    public record CreateReviewDTO(
        string? Content,
        int Rating,
        int ReviewerId,
        int? MediaId
    );
}