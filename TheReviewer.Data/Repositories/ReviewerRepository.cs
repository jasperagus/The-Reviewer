using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TheReviewer.Models;

namespace TheReviewer.Data.Repositories
{
    public class ReviewerRepository
    {
        public List<ReviewerModel> GetAll()
        {
            var query = "SELECT * from Reviewer";
            using var connection = new SqlConnection("Server=mssqlstud.fhict.local;Database=dbi580730_reviewer;User Id=dbi580730_reviewer;Password=jasper;TrustServerCertificate=true;");
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return new List<ReviewerModel>();
            }

            var reviewers = new List<ReviewerModel>();
            while (reader.Read())
            {
                var reviewer = new ReviewerModel(reader.GetInt32(0), reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)), reader.GetDateTime(3), reader.GetDateTime(4));
                reviewers.Add(reviewer);
            }
            return reviewers;
        }
    }
}
