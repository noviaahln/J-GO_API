using System;

namespace J_GO_API.Dtos
{
    public class DriverDto
    {
        public int id_driver { get; set; }
        public string nama_driver { get; set; }
        public string? jenis_kendaraan { get; set; }
        public string? no_telepon { get; set; }
        public string status_online { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }
}