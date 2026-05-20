namespace TheReviewer.Logic.Models
{
    public class ReviewModel
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public int Rating { get; private set; }
        public int ReviewerId { get; private set; }
        public int? MediaId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public ReviewModel(int id, string content, int rating, int reviewerId, int? mediaId, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Content = content;
            Rating = rating;
            ReviewerId = reviewerId;
            MediaId = mediaId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}