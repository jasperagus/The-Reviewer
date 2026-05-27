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
            const string query = "SELECT id, content, rating, reviewer_id, media_id, created_at, updated_at FROM Review";
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
                int? mediaId = reader.IsDBNull(reader.GetOrdinal("media_id"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("media_id"));

                var review = new GetReviewDTO(
                    reader.GetInt32(reader.GetOrdinal("id")),
                    reader.GetString(reader.GetOrdinal("content")),
                    reader.GetInt32(reader.GetOrdinal("rating")),
                    reader.GetInt32(reader.GetOrdinal("reviewer_id")),
                    mediaId,
                    reader.GetDateTime(reader.GetOrdinal("created_at")),
                    reader.GetDateTime(reader.GetOrdinal("updated_at"))
                );

                reviews.Add(review);
            }

            return reviews;
        }

        public void Add(CreateReviewDTO review)
        {
            const string query = "INSERT INTO Review (content, rating, reviewer_id, media_id, created_at, updated_at) VALUES (@content, @rating, @reviewer_id, @media_id, @created_at, @updated_at)";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", !string.IsNullOrWhiteSpace(review.Content) ? review.Content : string.Empty);
            command.Parameters.AddWithValue("@rating", review.Rating);
            command.Parameters.AddWithValue("@reviewer_id", review.ReviewerId);
            command.Parameters.AddWithValue("@media_id", review.MediaId.HasValue ? (object)review.MediaId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@created_at", DateTime.Now);
            command.Parameters.AddWithValue("@updated_at", DateTime.Now);

            connection.Open();
            command.ExecuteNonQuery();
        }

        // New: Get single review by id
        public GetReviewDTO? GetById(int id)
        {
            const string query = "SELECT id, content, rating, reviewer_id, media_id, created_at, updated_at FROM Review WHERE id = @id";
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            int? mediaId = reader.IsDBNull(reader.GetOrdinal("media_id"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("media_id"));

            var review = new GetReviewDTO(
                reader.GetInt32(reader.GetOrdinal("id")),
                reader.GetString(reader.GetOrdinal("content")),
                reader.GetInt32(reader.GetOrdinal("rating")),
                reader.GetInt32(reader.GetOrdinal("reviewer_id")),
                mediaId,
                reader.GetDateTime(reader.GetOrdinal("created_at")),
                reader.GetDateTime(reader.GetOrdinal("updated_at"))
            );

            return review;
        }

        // New: Update a review using UpdateReviewDTO (includes Id)
        public void Update(UpdateReviewDTO review)
        {
            const string query = @"
                UPDATE Review
                SET content = @content,
                    rating = @rating,
                    reviewer_id = @reviewer_id,
                    media_id = @media_id,
                    updated_at = @updated_at
                WHERE id = @id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@content", !string.IsNullOrWhiteSpace(review.Content) ? review.Content : string.Empty);
            command.Parameters.AddWithValue("@rating", review.Rating);
            command.Parameters.AddWithValue("@reviewer_id", review.ReviewerId);
            // UpdateReviewDTO.MediaId is non-nullable int in your DTO — if you want nullable, change DTO accordingly.
            command.Parameters.AddWithValue("@media_id", review.MediaId == 0 ? DBNull.Value : (object)review.MediaId);
            command.Parameters.AddWithValue("@updated_at", DateTime.Now);
            command.Parameters.AddWithValue("@id", review.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}