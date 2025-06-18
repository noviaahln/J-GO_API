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
        public bool CreatePengguna(Pengguna pengguna)
        {
            string query = @"INSERT INTO ""Pengguna"" (nama_lengkap, email, kata_sandi, jenis_pengguna) 
                             VALUES (@nama_lengkap, @email, @kata_sandi, @jenis_pengguna)";
            var parameters = new Dictionary<string, object>
            {
                { "@nama_lengkap", pengguna.nama_lengkap },
                { "@email", pengguna.email },
                { "@kata_sandi", pengguna.kata_sandi },
                { "@jenis_pengguna", pengguna.jenis_pengguna }
            };
            try
            {
                _dbHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Pengguna? GetByEmail(string email)
        {
            string query = "SELECT * FROM \"Pengguna\" WHERE email = @email";

            var parameters = new Dictionary<string, object>
            {
                { "@email", email }
            };

            var data = _dbHelper.ExecuteReader(query, parameters);

            if (data.Count > 0)
            {
                var row = data[0];
                return new Pengguna
                {
                    id_pengguna = Convert.ToInt32(row["id_pengguna"]),
                    nama_lengkap = Convert.ToString(row["nama_lengkap"]),
                    email = Convert.ToString(row["email"]),
                    kata_sandi = Convert.ToString(row["kata_sandi"]),
                    jenis_pengguna = Convert.ToString(row["jenis_pengguna"])
                };
            }

            return null;
        }
    }
}
