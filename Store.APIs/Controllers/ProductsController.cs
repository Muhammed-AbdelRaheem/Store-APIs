using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Servecies.Contract;

namespace Store.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]string? sort)
        {
            var result = await _productService.GetAllProductsAsync(sort);
            return Ok(result);
        }


        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null)
            {
                BadRequest("Invalid Id !!");
            }
            var result = await _productService.GetProductById(id.Value );

            if (result is null)
            {
                return
                NotFound($"The Product With ID : {id} Not Found In Db");
            }

            return Ok(result);
        }
    }
}





