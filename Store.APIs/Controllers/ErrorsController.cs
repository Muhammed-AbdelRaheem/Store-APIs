using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;

namespace Store.APIs.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {

        public IActionResult Error(int code)
        {

            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

        }


    }
}
