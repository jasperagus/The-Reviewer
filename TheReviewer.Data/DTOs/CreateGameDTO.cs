namespace TheReviewer.Data.DTOs
{
    public record CreateGameDTO(
        string Name,
        string Publisher,
        int Score
    );
}