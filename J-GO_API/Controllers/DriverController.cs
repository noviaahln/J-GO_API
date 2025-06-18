using J_GO_API.Contexts;
using J_GO_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace J_GO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly DriverContext _driverContext;
        private readonly OrderContext _orderContext;

        public DriverController(DriverContext driverContext, OrderContext orderContext)
        {
            _driverContext = driverContext;
            _orderContext = orderContext;
        }

        // GET: api/Driver/{id}
        [HttpGet("{id}")]
        public IActionResult GetDriver(int id)
        {
            var driver = _driverContext.GetDriver(id);
            if (driver == null)
                return NotFound(new { success = false, message = "Driver not found" });
            return Ok(driver);
        }

        // GET: api/Driver/orders/available
        [HttpGet("orders/available")]
        public IActionResult GetAvailableOrdersForDriver()
        {
            var orders = _orderContext.GetAvailableOrders();
            return Ok(new { data = orders });
        }

        // POST: api/Driver/orders/accept
        [HttpPost("orders/accept")]
        public IActionResult AcceptOrder([FromBody] AcceptOrderDto dto)
        {
            if (dto.orderId <= 0 || dto.driverId <= 0)
                return BadRequest(new { success = false, message = "orderId dan driverId wajib diisi" });

            _orderContext.AcceptOrder(dto.orderId, dto.driverId);
            return Ok(new { success = true, message = "Order accepted" });
        }
    }

    public class AcceptOrderDto
    {
        public int orderId { get; set; }
        public int driverId { get; set; }
    }
}