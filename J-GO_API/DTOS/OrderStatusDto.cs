using System;

namespace J_GO_API.Dtos
{
    public class OrderStatusDto
    {
        public int id_order { get; set; }
        public int? id_driver { get; set; }
        public int id_customer { get; set; }
        public string jenis_layanan { get; set; }
        public string lokasi_jemput { get; set; }
        public string lokasi_tujuan { get; set; }
        public double jarak { get; set; }
        public double harga { get; set; }
        public DateTime waktu_pemesanan { get; set; }
        public string status_order { get; set; }
        public string penjemputan { get; set; }
        public DriverDto? driver { get; set; } // opsional: info driver jika sudah ada
    }
}