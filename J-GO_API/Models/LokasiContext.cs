using J_GO_API.Helpers;

namespace J_GO_API.Models
{
    public class LokasiContext
    {
        private readonly SqlDBHelper _dbHelper;

        public LokasiContext(string connectionString)
        {
            _dbHelper = new SqlDBHelper(connectionString);
        }

        public List<Lokasi> ListLokasi()
        {
            var result = new List<Lokasi>();
            string query = "SELECT * FROM \"Lokasi\"";

            var data = _dbHelper.ExecuteReader(query);
            foreach (var row in data)
            {
                result.Add(new Lokasi
                {
                    id_lokasi = Convert.ToInt32(row["id_lokasi"]),
                    id_driver = Convert.ToInt32(row["id_driver"]),
                    waktu_update = (TimeSpan)row["waktu_update"],
                    longitude = Convert.ToDouble(row["longitude"]),
                    latitude = Convert.ToDouble(row["latitude"])
                });
            }

            return result;
        }

        public bool CreateLokasi(Lokasi loc)
        {
            string query = @"INSERT INTO ""Lokasi"" (id_driver, waktu_update, longitude, latitude) 
                             VALUES (@id_driver, @waktu_update, @longitude, @latitude)";
            var parameters = new Dictionary<string, object>
            {
                { "@id_driver", loc.id_driver },
                { "@waktu_update", loc.waktu_update },
                { "@longitude", loc.longitude },
                { "@latitude", loc.latitude }
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
    }
}
