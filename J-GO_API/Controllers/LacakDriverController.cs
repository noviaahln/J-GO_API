using Microsoft.AspNetCore.Mvc;
using J_GO_API.Helpers;
using J_GO_API.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace J_GO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverLocationController : ControllerBase
    {
        private readonly SqlDBHelper _dbHelper;

        public DriverLocationController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("koneksi");
            _dbHelper = new SqlDBHelper(connectionString);
        }

        [HttpPost]
        public IActionResult PostDriverLocation([FromBody] lokasi location)
        {
            if (location == null)
            {
                return BadRequest("Data lokasi tidak valid.");
            }

            string query = @"
                INSERT INTO ""Lokasi"" 
                (""id_driver"", ""waktu_update"", ""longitude"", ""latitude"") 
                VALUES 
                (@id_driver, @waktu_update, @longitude, @latitude)";

            var parameters = new Dictionary<string, object>
            {
                { "@id_driver", location.id_driver },
                { "@waktu_update", location.waktu_update },
                { "@longitude", location.longitude },
                { "@latitude", location.latitude }
            };

            try
            {
                _dbHelper.ExecuteNonQuery(query, parameters);
                return Ok(new { message = "Lokasi berhasil disimpan." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Terjadi kesalahan: {ex.Message}");
            }
        }
    }
}
