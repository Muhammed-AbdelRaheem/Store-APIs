using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;
using Store.Core.Dtos.Auth;
using Store.Core.Servecies.Contract;

namespace Store.APIs.Controllers
{


    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Login")] // POST : /Api/Accounts/Login
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {

            var user = await _userService.LoginAsync(loginDto);

            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            return Ok(user);
        }



        [HttpPost("Register")] // POST : /Api/Accounts/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            var user = await _userService.RegisterAsync(registerDto);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(user);
        }


    }
}
