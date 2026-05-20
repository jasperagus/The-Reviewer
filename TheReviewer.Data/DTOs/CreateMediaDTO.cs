namespace TheReviewer.Data.DTOs
{
    public record CreateMediaDTO(
        string Name,
        string Publisher,
        int Score,
        int TypeId
    );
}