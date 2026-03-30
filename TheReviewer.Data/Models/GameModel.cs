namespace TheReviewer.Models
{
    public class GameModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public int Score { get; private set; }
        public DateTime created_at { get; private set; }
        public DateTime updated_at { get; private set; }

        public GameModel(int id, string name, string publisher, int score, DateTime created_at, DateTime updated_at)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Score = score;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }
    }
}
