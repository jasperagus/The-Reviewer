namespace TheReviewer.Data.DTOs
{
    public record GetGameDTO(
        int Id,
        string Name,
        string Publisher,
        int Score,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}