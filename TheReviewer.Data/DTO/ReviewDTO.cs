namespace TheReviewer.Data.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int ReviewerId { get; set; }
        public int? FilmId { get; set; }
        public int? GameId { get; set; }
    }
}