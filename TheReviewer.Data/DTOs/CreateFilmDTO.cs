namespace TheReviewer.Data.DTOs
{
    public record CreateFilmDTO(
        string Name,
        string Publisher,
        int Score
    );
}