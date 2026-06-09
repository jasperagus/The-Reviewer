namespace TheReviewer.Logic.Models
{
    public class MediaModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public int Score { get; private set; }
        public int TypeId { get; private set; }
        public int? Episodes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public MediaModel(int id, string name, string publisher, int score, int typeId, int? episodes, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Score = score;
            TypeId = typeId;
            Episodes = episodes;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}