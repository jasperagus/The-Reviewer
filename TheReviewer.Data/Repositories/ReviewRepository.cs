using Microsoft.Data.SqlClient;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GetReviewDTO> GetAll()
        {
            const string query = "SELECT id, content, rating, reviewer_id, film_id, game_id, created_at, updated_at FROM Review";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<GetReviewDTO>();
            }

            var reviews = new List<GetReviewDTO>();
            while (reader.Read())
            {
                int? filmId = reader.IsDBNull(reader.GetOrdinal("film_id"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("film_id"));

                int? gameId = reader.IsDBNull(reader.GetOrdinal("game_id"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("game_id"));

                var review = new GetReviewDTO(
                    reader.GetInt32(reader.GetOrdinal("id")),
                    reader.GetString(reader.GetOrdinal("content")),
                    reader.GetInt32(reader.GetOrdinal("rating")),
                    reader.GetInt32(reader.GetOrdinal("reviewer_id")),
                    filmId,
                    gameId,
                    reader.GetDateTime(reader.GetOrdinal("created_at")),
                    reader.GetDateTime(reader.GetOrdinal("updated_at"))
                );

                reviews.Add(review);
            }

            return reviews;
        }

        public void Add(CreateReviewDTO review)
        {
            const string query = "INSERT INTO Review (content, rating, reviewer_id, film_id, game_id, created_at, updated_at) VALUES (@content, @rating, @reviewer_id, @film_id, @game_id, @created_at, @updated_at)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", review.Content);
            command.Parameters.AddWithValue("@rating", review.Rating);
            command.Parameters.AddWithValue("@reviewer_id", review.ReviewerId);
            command.Parameters.AddWithValue("@film_id", review.FilmId!);
            command.Parameters.AddWithValue("@game_id", review.GameId!);
            command.Parameters.AddWithValue("@created_at", DateTime.Now);
            command.Parameters.AddWithValue("@updated_at", DateTime.Now);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}