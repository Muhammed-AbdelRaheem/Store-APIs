using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Attributes;
using Store.APIs.Errors;
using Store.Core.Dtos;
using Store.Core.Helper;
using Store.Core.Servecies.Contract;
using Store.Core.Specifications.Products;

namespace Store.APIs.Controllers
{
    [Authorize]

    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }




        [ProducesResponseType(typeof(PaginationResponse<ProductDto>),StatusCodes.Status200OK)]
        [HttpGet]
        [Cached(100)]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var result = await _productService.GetAllProductsAsync(productSpec);
            return Ok(result);
        }



        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);
        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);
        }



        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null)
            {
                BadRequest(new ApiErrorResponse(400));
            }
            var result = await _productService.GetProductById(id.Value);

            if (result is null)
            {
                return
                NotFound(new ApiErrorResponse(404));
            }

            return Ok(result);
        }
    }
}





