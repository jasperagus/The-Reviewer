namespace TheReviewer.Models
{
    public class ReviewerModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }
    }
}
