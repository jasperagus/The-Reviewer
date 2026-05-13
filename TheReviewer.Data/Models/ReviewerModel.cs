namespace TheReviewer.Data.Models
{
    public class ReviewerModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public ReviewerModel(int id, string name, DateOnly birthdate, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}