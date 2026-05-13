namespace TheReviewer.Data.Models
{
    public class ReviewModel
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public int Rating { get; private set; }
        public int ReviewerId { get; private set; }
        public int? FilmId { get; private set; }
        public int? GameId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public ReviewModel(int id, string content, int rating, int reviewerId, int? filmId, int? gameId, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Content = content;
            Rating = rating;
            ReviewerId = reviewerId;
            FilmId = filmId;
            GameId = gameId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}