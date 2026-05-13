namespace TheReviewer.Data.Models
{
    public class FilmModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public int Score { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public FilmModel(int id, string name, string publisher, int score, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Score = score;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}