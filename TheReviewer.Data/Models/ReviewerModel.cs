namespace TheReviewer.Models
{
    public class ReviewerModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateOnly Birthdate { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }

        public ReviewerModel(int id, string name, DateOnly birthdate, DateTime created_at, DateTime updated_at)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            Created_at = created_at;
            Updated_at = updated_at;
        }
    }
}
