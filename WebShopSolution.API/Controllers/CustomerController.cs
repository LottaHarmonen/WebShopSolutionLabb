using Microsoft.AspNetCore.Mvc;
using WebShop.Services.User;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.Get(id);

            if (customer is null)
                return NotFound();

            return Ok(customer);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAll();
            if (customers is null)
                return NoContent();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(Customer customer)
        {
            if (customer is null)
                return BadRequest();

            await _customerService.Add(customer);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerService.Delete(id);
            return Ok();
        }

        //TODO Create a put that updates the user
    }
}
