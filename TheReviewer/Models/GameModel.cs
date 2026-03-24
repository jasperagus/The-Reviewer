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
    }
}
