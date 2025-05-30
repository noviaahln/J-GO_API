using Microsoft.AspNetCore.Mvc;
using J_GO_API.Models;

namespace J_GO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LacakDriverController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LacakDriverController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Lokasi>> GetAll()
        {
            string connString = _configuration.GetConnectionString("koneksi");
            LokasiContext context = new LokasiContext(connString);
            return Ok(context.ListLokasi());
        }

        [HttpPost]
        public ActionResult Create([FromBody] Lokasi loc)
        {
            string connString = _configuration.GetConnectionString("koneksi");
            LokasiContext context = new LokasiContext(connString);

            bool result = context.CreateLokasi(loc);

            if (result)
                return Ok(new { message = "Data lokasi berhasil ditambahkan." });
            else
                return BadRequest(new { message = "Gagal menambahkan data lokasi." });
        }
    }
}
