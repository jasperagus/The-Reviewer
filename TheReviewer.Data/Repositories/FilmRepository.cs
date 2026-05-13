using Microsoft.Data.SqlClient;
using TheReviewer.Data.Models;

namespace TheReviewer.Data.Repositories
{
    public class FilmRepository
    {
        private readonly string _connectionString;

        public FilmRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<FilmModel> GetAll()
        {
            const string query = "SELECT id, name, publisher, score, created_at, updated_at from Film";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<FilmModel>();
            }

            var films = new List<FilmModel>();
            while (reader.Read())
            {
                var film = new FilmModel(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3),
                    reader.GetDateTime(4),
                    reader.GetDateTime(5)
                );

                films.Add(film);
            }

            return films;
        }
    }
}