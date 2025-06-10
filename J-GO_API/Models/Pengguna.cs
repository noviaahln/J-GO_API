namespace J_GO_API.Models
{
    public class Pengguna
    {
        public int id_pengguna { get; set; }
        public string nama_lengkap { get; set; }
        public string jenis_pengguna { get; set; } // "driver" atau "customer"
    }
}
