namespace J_GO_API.Models
{
    public class OrderModel
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
        public string status_order { get; set; }   // "aktif" / "inaktif"
        public string penjemputan { get; set; }    // "pending" / "pick up" / "done"
    }
}