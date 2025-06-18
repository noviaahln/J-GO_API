namespace J_GO_API.Models
{
    public class DriverModel
    {
        public int id_driver { get; set; }
        public int? id_pengguna { get; set; }
        public string jenis_kendaraan { get; set; }
        public string status_online { get; set; }
        public string nama_driver { get; set; }
        public string no_telepon { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}