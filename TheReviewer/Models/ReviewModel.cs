namespace TheReviewer.Models
{
    public class ReviewModel
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public int Rating { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }
    }
}
