using J_GO_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace J_GO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PenggunaController : Controller
    {
        private readonly IConfiguration _configuration;

        public PenggunaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pengguna>> GetAll()
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);
            return Ok(context.ListPengguna());
        }

        // Tambahan: GET api/pengguna/{id}
        [HttpGet("{email}")]
        public ActionResult<Pengguna> GetByEmail(string email)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            PenggunaContext context = new PenggunaContext(connString);
            var pengguna = context.GetByEmail(email);

            if (pengguna == null)
            {
                return NotFound(new { message = "Pengguna tidak ditemukan." });
            }

            return Ok(pengguna);
        }
    }
}
