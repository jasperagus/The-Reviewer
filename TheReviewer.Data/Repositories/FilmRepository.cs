using Microsoft.Data.SqlClient;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly string _connectionString;

        public FilmRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GetFilmDTO> GetAll()
        {
            const string query = "SELECT id, name, publisher, score, created_at, updated_at from Film";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<GetFilmDTO>();
            }

            var films = new List<GetFilmDTO>();
            while (reader.Read())
            {
                var film = new GetFilmDTO(
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