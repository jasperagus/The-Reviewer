using Microsoft.Data.SqlClient;
using TheReviewer.Data.Models;

namespace TheReviewer.Data.Repositories
{
    public class GameRepository
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GameModel> GetAll()
        {
            const string query = "SELECT id, name, publisher, score, created_at, updated_at from Game";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<GameModel>();
            }

            var games = new List<GameModel>();
            while (reader.Read())
            {
                var game = new GameModel(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3),
                    reader.GetDateTime(4),
                    reader.GetDateTime(5)
                );

                games.Add(game);
            }

            return games;
        }
    }
}