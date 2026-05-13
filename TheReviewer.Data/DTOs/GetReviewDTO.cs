namespace TheReviewer.Data.DTOs
{
    public record GetReviewDTO(
        int Id,
        string Content,
        int Rating,
        int ReviewerId,
        int? FilmId,
        int? GameId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}