namespace TheReviewer.Data.DTOs
{
    public record GetMediaDTO(
        int Id,
        string Name,
        string Publisher,
        int Score,
        int TypeId,
        int? Episodes,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}