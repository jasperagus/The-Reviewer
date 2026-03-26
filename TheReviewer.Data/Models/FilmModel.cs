namespace TheReviewer.Models
{
    public class FilmModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public int Score { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }

        public FilmModel(int id, string name, string publisher, int score, DateTime created_at, DateTime updated_at)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            this.Score = score;
            Created_at = created_at;
            Updated_at = updated_at;
        }
    }
}

