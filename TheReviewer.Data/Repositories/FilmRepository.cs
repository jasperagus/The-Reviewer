using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TheReviewer.Models;

namespace TheReviewer.Data.Repositories
{
    public class FilmRepository
    {
        private  string _connection;

        public FilmRepository(string connectionString)
        {
            _connection = connectionString;
        }

        public  List<FilmModel> GetAll()
        {
            var query = "SELECT * from Film";
            using var connection = new SqlConnection(_connection);
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader =  command.ExecuteReader();
            if(!reader.HasRows)
            {
                return new List<FilmModel>();
            }

            var films = new List<FilmModel>();
            while( reader.Read())
            {
                var film = new FilmModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetDateTime(5));
                films.Add(film);
            }
            return films;
        }
    }
}
