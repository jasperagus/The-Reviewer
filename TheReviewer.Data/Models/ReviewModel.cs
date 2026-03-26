namespace TheReviewer.Models
{
    public class ReviewModel
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public int Rating { get; private set; }
        public int ReviewerId { get; private set; }
        public int? FilmId { get; private set; }
        public int? GameId { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }

        public ReviewModel(int id, string content, int rating, int reviewerId, int? filmId, int? gameId, DateTime created_at, DateTime updated_at)
        {
            Id = id;
            Content = content;
            Rating = rating;
            ReviewerId = reviewerId;
            FilmId = filmId;
            GameId = gameId;
            Created_at = created_at;
            Updated_at = updated_at;
        }
    }
}
