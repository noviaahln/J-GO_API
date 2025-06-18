using Npgsql;
using System.Collections.Generic;

namespace J_GO_API.Helpers
{
    public class SqlDBHelper
    {
        private readonly string _connectionString;

        public SqlDBHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(query, connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            command.ExecuteNonQuery();
        }

        public List<Dictionary<string, object>> ExecuteReader(string query, Dictionary<string, object> parameters)
        {
            var result = new List<Dictionary<string, object>>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(query, connection);
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                result.Add(row);
            }

            return result;
        }

        public List<Dictionary<string, object>> ExecuteReader(string query)
        {
            var result = new List<Dictionary<string, object>>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                result.Add(row);
            }

            return result;
        }
    }
}