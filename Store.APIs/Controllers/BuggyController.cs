using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;
using Store.Repository.Data.Contexts;

namespace Store.APIs.Controllers
{

    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }




        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFoundRequestError() //NotFound Error
        {
            var brand =await _context.Brands.FindAsync(100);

            if (brand is null) return NotFound(new ApiErrorResponse(404));   

            return Ok(brand);
        }


        [HttpGet("badrequest")]
        public IActionResult GetBadRequestError() //BadRequest Error
        {
            return BadRequest(new ApiErrorResponse(400));
            
        }

        [HttpGet("unauthorized")]
        public  IActionResult GetUnAuthorizedError() //UnAuthorized Error 
        {

            return Unauthorized(new ApiErrorResponse(401));
        }





        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerError() //Server Error
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandToString = brand.ToString();

            return Ok(brand);
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequestError(int id) //Validation Error 
        {

            return Ok();
        }


    }
}
