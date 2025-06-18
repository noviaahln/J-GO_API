namespace J_GO_API.Models
{
    public class Riwayat
    {
        public int id_order { get; set; }
        public string jenis_layanan { get; set; }
        public string lokasi_jemput { get; set; }
        public string lokasi_tujuan { get; set; }
        public int jarak { get; set; }
        public int harga { get; set; }
        public DateTime waktu_pemesanan { get; set; }
        public string status_order { get; set; }
    }
}
