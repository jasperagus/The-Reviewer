namespace TheReviewer.Models
{
    public class FilmModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public int score { get; private set; }
        public DateTime Created_at { get; private set; }
        public DateTime Updated_at { get; private set; }

        public List<FilmModel> Films { get; set; } = new List<FilmModel>();
    }
}
