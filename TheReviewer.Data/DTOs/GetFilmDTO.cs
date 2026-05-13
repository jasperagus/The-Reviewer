namespace TheReviewer.Data.DTOs
{
    public record GetFilmDTO(
        int Id,
        string Name,
        string Publisher,
        int Score,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}