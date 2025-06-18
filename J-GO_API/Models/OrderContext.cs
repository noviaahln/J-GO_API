using J_GO_API.Models;
using J_GO_API.Helpers;

namespace J_GO_API.Contexts
{
    public class OrderContext
    {
        private readonly SqlDBHelper _db;
        public OrderContext(SqlDBHelper db)
        {
            _db = db;
        }

        public int CreateOrder(OrderModel order)
        {
            var sql = @"INSERT INTO ""Order"" (id_customer, jenis_layanan, lokasi_jemput, lokasi_tujuan, jarak, harga, waktu_pemesanan, status_order, penjemputan)
                        VALUES (@id_customer, @jenis_layanan, @lokasi_jemput, @lokasi_tujuan, @jarak, @harga, @waktu_pemesanan, @status_order, @penjemputan)
                        RETURNING id_order";
            var parameters = new Dictionary<string, object>
            {
                ["id_customer"] = order.id_customer,
                ["jenis_layanan"] = order.jenis_layanan,
                ["lokasi_jemput"] = order.lokasi_jemput,
                ["lokasi_tujuan"] = order.lokasi_tujuan,
                ["jarak"] = order.jarak,
                ["harga"] = order.harga,
                ["waktu_pemesanan"] = order.waktu_pemesanan,
                ["status_order"] = order.status_order,
                ["penjemputan"] = order.penjemputan
            };
            return Convert.ToInt32(_db.ExecuteReader(sql, parameters));
        }

        public OrderModel? GetOrder(int id_order)
        {
            var sql = @"SELECT * FROM ""Order"" WHERE id_order = @id_order";
            var parameters = new Dictionary<string, object> { ["id_order"] = id_order };
            var result = _db.ExecuteReader(sql, parameters);
            if (result.Count == 0) return null;
            var row = result[0];
            return Map(row);
        }

        public List<OrderModel> GetAvailableOrders()
        {
            var sql = @"SELECT * FROM ""Order"" WHERE status_order = 'aktif' AND penjemputan = 'pending' AND id_driver IS NULL";
            var result = _db.ExecuteReader(sql);
            return result.Select(Map).ToList();
        }

        public void UpdateOrderStatus(int id_order, string status)
        {
            var sql = @"UPDATE ""Order"" SET status_order = @status WHERE id_order = @id_order";
            var parameters = new Dictionary<string, object>
            {
                ["status"] = status,
                ["id_order"] = id_order
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        public void UpdatePenjemputan(int id_order, string penjemputan)
        {
            var sql = @"UPDATE ""Order"" SET penjemputan = @penjemputan WHERE id_order = @id_order";
            var parameters = new Dictionary<string, object>
            {
                ["penjemputan"] = penjemputan,
                ["id_order"] = id_order
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        // INI YANG DIBENARKAN!
        public void AcceptOrder(int id_order, int id_driver)
        {
            var sql = @"UPDATE ""Order"" 
                        SET penjemputan = 'pick up', id_driver = @id_driver, status_order = 'dijemput'
                        WHERE id_order = @id_order";
            var parameters = new Dictionary<string, object>
            {
                ["id_driver"] = id_driver,
                ["id_order"] = id_order
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        private OrderModel Map(Dictionary<string, object> row)
        {
            return new OrderModel
            {
                id_order = Convert.ToInt32(row["id_order"]),
                id_driver = row["id_driver"] is DBNull ? null : Convert.ToInt32(row["id_driver"]),
                id_customer = Convert.ToInt32(row["id_customer"]),
                jenis_layanan = row["jenis_layanan"].ToString(),
                lokasi_jemput = row["lokasi_jemput"].ToString(),
                lokasi_tujuan = row["lokasi_tujuan"].ToString(),
                jarak = Convert.ToDouble(row["jarak"]),
                harga = Convert.ToDouble(row["harga"]),
                waktu_pemesanan = Convert.ToDateTime(row["waktu_pemesanan"]),
                status_order = row["status_order"].ToString(),
                penjemputan = row["penjemputan"].ToString()
            };
        }
    }
}