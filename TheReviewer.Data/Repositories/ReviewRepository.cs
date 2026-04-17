using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TheReviewer.Data.DTO;
using TheReviewer.Models;

namespace TheReviewer.Data.Repositories
{
    public class ReviewRepository
    {
        private  string _connection;
        public ReviewRepository(string connectionString)
        {
            _connection = connectionString;
            
        }
        public List<ReviewModel> GetAll()
        {
            var query = "SELECT * FROM Review";
            using var connection = new SqlConnection(_connection);
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<ReviewModel>();
            }

            var reviews = new List<ReviewModel>();
            while (reader.Read())
            {
                int? filmId = reader.IsDBNull(reader.GetOrdinal("film_id")) ? null : reader.GetInt32(reader.GetOrdinal("film_id"));
                int? gameId = reader.IsDBNull(reader.GetOrdinal("game_id")) ? null : reader.GetInt32(reader.GetOrdinal("game_id"));
                var review = new ReviewModel(
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

        public void Add(ReviewDTO review)
        {
            var query = "INSERT INTO Review (content, rating, reviewer_id, film_id, game_id, created_at, updated_at) VALUES (@content, @rating, @reviewer_id, @film_id, @game_id, @created_at, @updated_at)";
            using var connection = new SqlConnection(_connection);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@content", review.Content);
            command.Parameters.AddWithValue("@rating", review.Rating);
            command.Parameters.AddWithValue("@reviewer_id", review.ReviewerId);
            command.Parameters.AddWithValue("@film_id", (object)review.FilmId ?? DBNull.Value);
            command.Parameters.AddWithValue("@game_id", (object)review.GameId ?? DBNull.Value);
            command.Parameters.AddWithValue("@created_at", DateTime.Now);
            command.Parameters.AddWithValue("@updated_at", DateTime.Now);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
