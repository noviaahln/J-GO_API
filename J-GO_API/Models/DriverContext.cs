using J_GO_API.Models;
using J_GO_API.Helpers;

namespace J_GO_API.Contexts
{
    public class DriverContext
    {
        private readonly SqlDBHelper _db;
        public DriverContext(SqlDBHelper db)
        {
            _db = db;
        }

        public DriverModel? GetDriver(int id_driver)
        {
            var sql = @"SELECT * FROM ""Driver"" WHERE id_driver = @id_driver";
            var parameters = new Dictionary<string, object> { ["id_driver"] = id_driver };
            var result = _db.ExecuteReader(sql, parameters);
            if (result.Count == 0) return null;
            var row = result[0];
            return Map(row);
        }

        private DriverModel Map(Dictionary<string, object> row)
        {
            return new DriverModel
            {
                id_driver = Convert.ToInt32(row["id_driver"]),
                id_pengguna = row["id_pengguna"] is DBNull ? null : Convert.ToInt32(row["id_pengguna"]),
                jenis_kendaraan = row["jenis_kendaraan"].ToString(),
                status_online = row["status_online"].ToString(),
                nama_driver = row["nama_driver"].ToString(),
                no_telepon = row["no_telepon"].ToString(),
                latitude = row["latitude"] is DBNull ? null : Convert.ToDouble(row["latitude"]),
                longitude = row["longitude"] is DBNull ? null : Convert.ToDouble(row["longitude"]),
                created_at = row["created_at"] is DBNull ? null : Convert.ToDateTime(row["created_at"]),
                updated_at = row["updated_at"] is DBNull ? null : Convert.ToDateTime(row["updated_at"])
            };
        }
    }
}