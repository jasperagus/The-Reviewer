using Microsoft.Data.SqlClient;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories;

public class AccountRepository(string connectionString) : IAccountRepository
{
    public GetAccountDTO? GetByEmail(string email)
    {
        const string query = """
                             SELECT id, email, passwordhash, created_at, updated_at
                             FROM [Reviewer]
                             WHERE email = @Email
                             """;

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Email", email);

        connection.Open();
        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new GetAccountDTO(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
            reader.GetDateTime(3),
            reader.IsDBNull(4) ? null : reader.GetDateTime(4)
        );
    }

    public GetAccountDTO Create(CreateAccountDTO account)
    {
        const string query = """
                             INSERT INTO [Reviewer] (name, birthdate, email, passwordhash, created_at, updated_at)
                             OUTPUT INSERTED.id, INSERTED.email, INSERTED.passwordhash, INSERTED.created_at, INSERTED.updated_at
                             VALUES (@Name, @Birthdate, @Email, @PasswordHash, @CreatedAt, @CreatedAt)
                             """;

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", GetDefaultReviewerName(account.Email));
        command.Parameters.AddWithValue("@Birthdate", new DateTime(1900, 1, 1));
        command.Parameters.AddWithValue("@Email", account.Email);
        command.Parameters.AddWithValue("@PasswordHash", account.PasswordHash);
        command.Parameters.AddWithValue("@CreatedAt", account.CreatedAt);

        connection.Open();
        using var reader = command.ExecuteReader();
        reader.Read();

        return new GetAccountDTO(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetDateTime(3),
            reader.IsDBNull(4) ? null : reader.GetDateTime(4)
        );
    }

    private static string GetDefaultReviewerName(string email)
    {
        var atIndex = email.IndexOf('@');

        return atIndex > 0 ? email[..atIndex] : email;
    }
}
