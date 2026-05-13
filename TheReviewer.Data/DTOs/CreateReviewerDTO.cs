namespace TheReviewer.Data.DTOs
{
    public record CreateReviewerDTO(
        string Name,
        DateOnly Birthdate
    );
}