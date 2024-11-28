using Microsoft.AspNetCore.Mvc;
using WebShop.DataAccess.Repositories.Product;
using WebShop.Services.Product;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product> products = await _productService.GetAll();

            return Ok(products);
        }


        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {

            if (product == null)
                return BadRequest();

            await _productService.AddProduct(product);

            return Ok("Product added");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            if (product == null)
                return BadRequest();

            await _productService.Update(product);
            return Ok();

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(Product product)
        {
            if (product == null)
                return BadRequest();

            await _productService.Delete(product.Id);
            return Ok();
        }

    }
}
