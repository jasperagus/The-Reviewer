using Microsoft.Data.SqlClient;
using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly string _connectionString;

        public MediaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GetMediaDTO> GetAll()
        {
            const string query = "SELECT id, name, publisher, score, type_id, created_at, updated_at FROM Media";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<GetMediaDTO>();
            }

            var media = new List<GetMediaDTO>();
            while (reader.Read())
            {
                var item = new GetMediaDTO(
                    reader.GetInt32(0),      // id
                    reader.GetString(1),     // name
                    reader.GetString(2),     // publisher
                    reader.GetInt32(3),      // score
                    reader.GetInt32(4),      // type_id
                    reader.GetDateTime(5),   // created_at
                    reader.GetDateTime(6)    // updated_at
                );

                media.Add(item);
            }

            return media;
        }

        public List<GetMediaDTO> GetByType(int typeId)
        {
            const string query = "SELECT id, name, publisher, score, type_id, created_at, updated_at FROM Media WHERE type_id = @typeId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@typeId", typeId);
            connection.Open();

            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<GetMediaDTO>();
            }

            var media = new List<GetMediaDTO>();
            while (reader.Read())
            {
                var item = new GetMediaDTO(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4),
                    reader.GetDateTime(5),
                    reader.GetDateTime(6)
                );

                media.Add(item);
            }

            return media;
        }
    }
}