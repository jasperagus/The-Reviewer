namespace TheReviewer.Data.DTOs;

public record UpdateReviewDTO(
    int Id,
    string? Content,
    int Rating,
    int ReviewerId,
    int MediaId
    );