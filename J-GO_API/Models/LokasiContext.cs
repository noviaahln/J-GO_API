using J_GO_API.Helpers;
using System;
using System.Collections.Generic;

namespace J_GO_API.Models
{
    public class LokasiContext
    {
        private readonly SqlDBHelper _dbHelper;

        public LokasiContext(string connectionString)
        {
            _dbHelper = new SqlDBHelper(connectionString);
        }

        // CREATE (otomatis ambil id_order terakhir dari driver)
        public bool CreateLokasi(Lokasi loc)
        {
            try
            {
                // Cari id_driver dari nama_driver
                string driverQuery = @"SELECT id_driver FROM ""Driver"" WHERE nama_driver = @nama_driver";
                var driverParams = new Dictionary<string, object> { { "@nama_driver", loc.nama_driver } };
                var driverData = _dbHelper.ExecuteReader(driverQuery, driverParams);

                if (driverData.Count == 0) return false; // nama_driver tidak ditemukan

                int id_driver = Convert.ToInt32(driverData[0]["id_driver"]);

                // Ambil id_order terakhir dari driver (jika ada)
                string orderQuery = @"SELECT id_order FROM ""Order"" WHERE id_driver = @id_driver ORDER BY id_order DESC LIMIT 1";
                var orderParams = new Dictionary<string, object> { { "@id_driver", id_driver } };
                var orderData = _dbHelper.ExecuteReader(orderQuery, orderParams);
                int? id_order = orderData.Count > 0 ? Convert.ToInt32(orderData[0]["id_order"]) : (int?)null;

                // Insert ke tabel Lokasi
                string query = @"INSERT INTO ""Lokasi"" (id_driver, id_order, waktu_update, longitude, latitude) 
                                 VALUES(@id_driver, @id_order, @waktu_update, @longitude, @latitude);";
                var parameters = new Dictionary<string, object>
                {
                    { "@id_driver", id_driver },
                    { "@id_order", id_order ?? (object)DBNull.Value },
                    { "@waktu_update", loc.waktu_update },
                    { "@longitude", loc.longitude },
                    { "@latitude", loc.latitude }
                };

                _dbHelper.ExecuteNonQuery(query, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // READ BY id_order
        public List<Lokasi> GetLokasiByOrderId(int idOrder)
        {
            var result = new List<Lokasi>();

            string query = @"
                SELECT l.id_lokasi, l.id_driver, l.waktu_update, l.longitude, l.latitude, 
                       d.nama_driver, l.id_order
                FROM ""Lokasi"" l
                JOIN ""Driver"" d ON l.id_driver = d.id_driver
                WHERE l.id_order = @id_order
                ORDER BY l.waktu_update DESC";


            var parameters = new Dictionary<string, object>
            {
                { "@id_order", idOrder }
            };

            var data = _dbHelper.ExecuteReader(query, parameters);
            foreach (var row in data)
            {
                result.Add(new Lokasi
                {
                    id_lokasi = Convert.ToInt32(row["id_lokasi"]),
                    id_driver = Convert.ToInt32(row["id_driver"]),
                    waktu_update = (TimeSpan)row["waktu_update"],
                    longitude = Convert.ToDouble(row["longitude"]),
                    latitude = Convert.ToDouble(row["latitude"]),
                    nama_driver = row["nama_driver"].ToString() ?? "",
                    id_order = row["id_order"] != DBNull.Value ? Convert.ToInt32(row["id_order"]) : (int?)null
                });
            }

            return result;
        }
        // READ BY id_driver
        public List<Lokasi> GetLokasiByDriverId(int idDriver)
        {
            var result = new List<Lokasi>();

            string query = @"
                SELECT l.id_lokasi, l.id_driver, l.waktu_update, l.longitude, l.latitude, 
                       d.nama_driver
                FROM ""Lokasi"" l
                JOIN ""Driver"" d ON l.id_driver = d.id_driver
                WHERE l.id_driver = @id_driver
                ORDER BY l.waktu_update DESC";


            var parameters = new Dictionary<string, object>
            {
                { "@id_driver", idDriver }
            };

            var data = _dbHelper.ExecuteReader(query, parameters);
            foreach (var row in data)
            {
                result.Add(new Lokasi
                {
                    id_lokasi = Convert.ToInt32(row["id_lokasi"]),
                    id_driver = Convert.ToInt32(row["id_driver"]),
                    waktu_update = (TimeSpan)row["waktu_update"],
                    longitude = Convert.ToDouble(row["longitude"]),
                    latitude = Convert.ToDouble(row["latitude"]),
                    nama_driver = row["nama_driver"].ToString() ?? "",
                });
            }

            return result;
        }
        // UPDATE
        public bool UpdateLokasi(Lokasi loc)
        {
            string query = @"UPDATE ""Lokasi"" 
                 SET id_driver = @id_driver, id_order = @id_order, waktu_update = @waktu_update,
                     longitude = @longitude, latitude = @latitude
                 WHERE id_lokasi = @id_lokasi";


            var parameters = new Dictionary<string, object>
            {
                { "@id_lokasi", loc.id_lokasi },
                { "@id_driver", loc.id_driver },
                { "@id_order", loc.id_order ?? (object)DBNull.Value },
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

        // DELETE
        public bool DeleteLokasi(int id)
        {
            string query = @"DELETE FROM ""Lokasi"" WHERE id_lokasi = @id_lokasi";
            var parameters = new Dictionary<string, object> { { "@id_lokasi", id } };

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
