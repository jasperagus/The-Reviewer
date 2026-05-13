using Microsoft.Data.SqlClient;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly string _connectionString;

        public ReviewerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GetReviewerDTO> GetAll()
        {
            const string query = "SELECT id, name, birthdate, created_at, updated_at from Reviewer";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<GetReviewerDTO>();
            }

            var reviewers = new List<GetReviewerDTO>();
            while (reader.Read())
            {
                var reviewer = new GetReviewerDTO(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    DateOnly.FromDateTime(reader.GetDateTime(2)),
                    reader.GetDateTime(3),
                    reader.GetDateTime(4)
                );

                reviewers.Add(reviewer);
            }

            return reviewers;
        }
    }
}