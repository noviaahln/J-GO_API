namespace J_GO_API.Models
{
    public class lokasi
    {
        public int id_lokasi { get; set; }
        public int id_driver { get; set; }
        public TimeSpan waktu_update { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
