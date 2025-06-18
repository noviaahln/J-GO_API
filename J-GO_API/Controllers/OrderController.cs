using Microsoft.AspNetCore.Mvc;
using J_GO_API.Contexts;
using J_GO_API.Models;
using J_GO_API.Dtos;

namespace J_GO_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _orderContext;

        public OrderController(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderCreateDto dto)
        {
            var order = new OrderModel
            {
                id_customer = dto.customerId,
                jenis_layanan = dto.jenisLayanan,
                lokasi_jemput = dto.lokasiJemput,
                lokasi_tujuan = dto.lokasiTujuan,
                jarak = dto.jarak,
                harga = dto.harga,
                waktu_pemesanan = DateTime.UtcNow,
                status_order = "aktif",
                penjemputan = "pending"
            };
            var orderId = _orderContext.CreateOrder(order);
            return Ok(new { success = true, data = new { orderId } });
        }

        [HttpGet("available")]
        public IActionResult GetAvailableOrders()
        {
            var orders = _orderContext.GetAvailableOrders();
            return Ok(new { data = orders });
        }

        [HttpPost("accept")]
        public IActionResult AcceptOrder([FromBody] AcceptOrderDto dto)
        {
            if (dto.orderId <= 0 || dto.driverId <= 0)
                return BadRequest(new { success = false, message = "orderId dan driverId wajib diisi" });

            _orderContext.AcceptOrder(dto.orderId, dto.driverId);
            return Ok(new { success = true });
        }

        [HttpGet("status/{id_order}")]
        public IActionResult GetOrderStatus(int id_order)
        {
            var order = _orderContext.GetOrder(id_order);
            if (order == null)
                return NotFound(new { success = false, message = "Order tidak ditemukan" });

            return Ok(new { data = order });
        }
    }
}