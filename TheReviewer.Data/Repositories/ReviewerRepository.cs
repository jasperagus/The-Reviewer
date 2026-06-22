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
            const string query = "SELECT id, name, email, passwordhash, birthdate, created_at, updated_at from Reviewer";

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
                    reader.GetString(2),
                    reader.GetString(3),
                    DateOnly.FromDateTime(reader.GetDateTime(4)),
                    reader.GetDateTime(5),
                    reader.GetDateTime(6)
                );

                reviewers.Add(reviewer);
            }

            return reviewers;
        }

        public GetReviewerDTO? GetByEmail(string email)
        {
            const string query = """
                                 SELECT id, name, email, passwordhash, birthdate, created_at, updated_at
                                 FROM [Reviewer]
                                 WHERE email = @Email
                                 """;

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new GetReviewerDTO(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                DateOnly.FromDateTime(reader.GetDateTime(4)),
                reader.GetDateTime(5),
                reader.GetDateTime(6)
            );
        }

        public GetReviewerDTO Create(CreateReviewerDTO reviewer)
        {
            const string query = """
                                 INSERT INTO [Reviewer] (name, birthdate, email, passwordhash, created_at, updated_at)
                                 OUTPUT INSERTED.id, INSERTED.name, INSERTED.email, INSERTED.passwordhash, INSERTED.birthdate, INSERTED.created_at, INSERTED.updated_at
                                 VALUES (@Name, @Birthdate, @Email, @PasswordHash, @CreatedAt, @CreatedAt)
                                 """;

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", reviewer.Name);
            command.Parameters.AddWithValue("@Birthdate", new DateTime(1900, 1, 1));
            command.Parameters.AddWithValue("@Email", reviewer.Email);
            command.Parameters.AddWithValue("@PasswordHash", reviewer.PasswordHash);
            command.Parameters.AddWithValue("@CreatedAt", reviewer.CreatedAt);

            connection.Open();
            using var reader = command.ExecuteReader();
            reader.Read();

            return new GetReviewerDTO(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                DateOnly.FromDateTime(reader.GetDateTime(4)),
                reader.GetDateTime(5),
                reader.GetDateTime(6)
            );
        }
    }
}
