using Microsoft.AspNetCore.Mvc;
using J_GO_API.Models;

namespace J_GO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LokasiController : ControllerBase
    {
        private readonly LokasiContext _context;

        public LokasiController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("koneksi") ?? "";
            _context = new LokasiContext(connectionString);
        }

        // CREATE (otomatis isi id_order dari driver)
        [HttpPost]
        public IActionResult CreateLokasi([FromBody] Lokasi lokasi)
        {
            bool success = _context.CreateLokasi(lokasi);
            if (success) return Ok(new { message = "Lokasi berhasil ditambahkan." });
            return BadRequest(new { message = "Gagal menambahkan lokasi." });
        }

        // GET ALL BY ID ORDER
        [HttpGet("by-order/{id_order}")]
        public IActionResult GetLokasiByOrder(int id_order)
        {
            var data = _context.GetLokasiByOrderId(id_order);
            if (data == null || data.Count == 0)
                return NotFound(new { message = "Tidak ada lokasi untuk order tersebut." });

            return Ok(data);
        }
        // GET ALL BY ID DRIVER
        [HttpGet("by-driver/{id_driver}")]
        public IActionResult GetLokasiByDriver(int id_driver)
        {
            var data = _context.GetLokasiByDriverId(id_driver);
            if (data == null || data.Count == 0)
                return NotFound(new { message = "Tidak ada lokasi untuk order tersebut." });

            return Ok(data);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateLokasi(int id, [FromBody] Lokasi lokasi)
        {
            lokasi.id_lokasi = id;
            bool success = _context.UpdateLokasi(lokasi);
            if (success) return Ok(new { message = "Lokasi berhasil diperbarui." });
            return BadRequest(new { message = "Gagal memperbarui lokasi." });
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteLokasi(int id)
        {
            bool success = _context.DeleteLokasi(id);
            if (success) return Ok(new { message = "Lokasi berhasil dihapus." });
            return BadRequest(new { message = "Gagal menghapus lokasi." });
        }
    }
}
