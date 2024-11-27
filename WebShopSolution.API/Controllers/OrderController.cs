using Microsoft.AspNetCore.Mvc;
using WebShop.Services.Order;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAll();
            if (orders is null)
                return NoContent();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderService.Get(id);
            if (order is null)
                return NoContent();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewOrder(Order order)
        {
            if (order is null)
                return BadRequest();

            await _orderService.Add(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Order order)
        {
            if (order is null)
                return BadRequest();

            await _orderService.Update(order);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.Delete(id);
            return Ok();
        }
    }
}
