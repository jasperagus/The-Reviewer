using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TheReviewer.Models;

namespace TheReviewer.Data.Repositories
{
    public class ReviewRepository
    {
        public List<ReviewModel> GetAll()
        {
            var query = "SELECT * FROM Review";
            using var connection = new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi580730_reviewer;User Id=dbi580730_reviewer;Password=jasper;TrustServerCertificate=true;");
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
    }
}
