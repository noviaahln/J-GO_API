using System.Text.Json.Serialization;

namespace J_GO_API.Dtos
{
    public class OrderCreateDto
    {
        [JsonPropertyName("customerId")]
        public int customerId { get; set; }

        [JsonPropertyName("jenisLayanan")]
        public string jenisLayanan { get; set; }

        [JsonPropertyName("lokasiJemput")]
        public string lokasiJemput { get; set; }

        [JsonPropertyName("lokasiTujuan")]
        public string lokasiTujuan { get; set; }

        [JsonPropertyName("jarak")]
        public double jarak { get; set; }

        [JsonPropertyName("harga")]
        public double harga { get; set; }
    }
}