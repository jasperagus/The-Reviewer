using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TheReviewer.Models;

namespace TheReviewer.Data.Repositories
{
    public class GameRepository
    {
        private  string _connection;
        public GameRepository(string connectionString)
        {
            _connection = connectionString;
            
        }
        public List<GameModel> GetAll()
        {
            var query = "SELECT * from Game";
            using var connection = new SqlConnection(_connection);
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
                var game = new GameModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5));
                games.Add(game);
            }
            return games;
        }
    }
}
