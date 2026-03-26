using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TheReviewer.Models;

namespace TheReviewer.Data.Repositories
{
    public class GameRepository
    {
        public List<GameModel> GetAll()
        {
            var query = "SELECT * from Game";
            using var connection = new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi580730_reviewer;User Id=dbi580730_reviewer;Password=jasper;TrustServerCertificate=true;");
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
