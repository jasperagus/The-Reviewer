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
            const string query = @"
                SELECT m.id, m.name, m.publisher, m.score, m.type_id, s.episodes, m.created_at, m.updated_at
                FROM Media m
                LEFT JOIN [Show] s ON s.media_id = m.id";

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
                    reader.IsDBNull(5) ? null : reader.GetInt32(5), // episodes
                    reader.GetDateTime(6),   // created_at
                    reader.GetDateTime(7)    // updated_at
                );

                media.Add(item);
            }

            return media;
        }

        public List<GetMediaDTO> GetByType(int typeId)
        {
            const string query = @"
                SELECT m.id, m.name, m.publisher, m.score, m.type_id, s.episodes, m.created_at, m.updated_at
                FROM Media m
                LEFT JOIN [Show] s ON s.media_id = m.id
                WHERE m.type_id = @typeId";

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
                    reader.IsDBNull(5) ? null : reader.GetInt32(5),
                    reader.GetDateTime(6),
                    reader.GetDateTime(7)
                );

                media.Add(item);
            }

            return media;
        }
    }
}