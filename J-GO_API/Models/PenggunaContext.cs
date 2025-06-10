using J_GO_API.Helpers;
using System;
using System.Collections.Generic;

namespace J_GO_API.Models
{
    public class PenggunaContext
    {
        private readonly SqlDBHelper _dbHelper;

        public PenggunaContext(string connectionString)
        {
            _dbHelper = new SqlDBHelper(connectionString);
        }

        public List<Pengguna> ListPengguna()
        {
            var result = new List<Pengguna>();
            string query = "SELECT * FROM \"Pengguna\"";

            var data = _dbHelper.ExecuteReader(query);
            foreach (var row in data)
            {
                result.Add(new Pengguna
                {
                    id_pengguna = Convert.ToInt32(row["id_pengguna"]),
                    nama_lengkap = Convert.ToString(row["nama_lengkap"]),
                    jenis_pengguna = Convert.ToString(row["jenis_pengguna"])
                });
            }

            return result;
        }

        public Pengguna? GetById(int id)
        {
            string query = "SELECT * FROM \"Pengguna\" WHERE id_pengguna = @id_pengguna";

            var parameters = new Dictionary<string, object>
            {
                { "@id_pengguna", id }
            };

            var data = _dbHelper.ExecuteReader(query, parameters);

            if (data.Count > 0)
            {
                var row = data[0];
                return new Pengguna
                {
                    id_pengguna = Convert.ToInt32(row["id_pengguna"]),
                    nama_lengkap = Convert.ToString(row["nama_lengkap"]),
                    jenis_pengguna = Convert.ToString(row["jenis_pengguna"])
                };
            }

            return null;
        }
    }
}
