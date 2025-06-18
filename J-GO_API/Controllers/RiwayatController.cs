using Microsoft.AspNetCore.Mvc;
using J_GO_API.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace J_GO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiwayatController : ControllerBase
    {
        private readonly RiwayatContext _context;

        public RiwayatController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("koneksi");
            _context = new RiwayatContext(connectionString);
        } 

        // GET: api/Riwayat
        [HttpGet]
        public ActionResult<IEnumerable<Riwayat>> GetAll()
        {
            var riwayatList = _context.GetSemuaRiwayat();
            return Ok(riwayatList);
        }

        // GET: api/Riwayat/5
        [HttpGet("{id}")]
        public ActionResult<Riwayat> GetById(int id)
        {
            var riwayat = _context.GetRiwayatById(id);
            if (riwayat == null)
                return NotFound(new { message = $"Riwayat dengan id_order {id} tidak ditemukan." });

            return Ok(riwayat);
        }

        // POST: api/Riwayat
        [HttpPost]
        public ActionResult Tambah([FromBody] Riwayat riwayat)
        {
            try
            {
                _context.TambahRiwayat(riwayat);
                return Ok(new { message = "Riwayat berhasil ditambahkan." });
            }
            catch
            {
                return BadRequest(new { message = "Gagal menambahkan riwayat." });
            }
        }

        // PUT: api/Riwayat/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Riwayat riwayat)
        {
            var existing = _context.GetRiwayatById(id);
            if (existing == null)
                return NotFound(new { message = $"Riwayat dengan id_order {id} tidak ditemukan." });

            riwayat.id_order = id;

            try
            {
                _context.UpdateRiwayat(riwayat);
                return Ok(new { message = "Riwayat berhasil diupdate." });
            }
            catch
            {
                return BadRequest(new { message = "Gagal mengupdate riwayat." });
            }
        }

        // DELETE: api/Riwayat/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existing = _context.GetRiwayatById(id);
            if (existing == null)
                return NotFound(new { message = $"Riwayat dengan id_order {id} tidak ditemukan." });

            try
            {
                _context.HapusRiwayat(id);
                return Ok(new { message = "Riwayat berhasil dihapus." });
            }
            catch
            {
                return BadRequest(new { message = "Gagal menghapus riwayat." });
            }
        }
    }
}
